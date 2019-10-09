using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using AutoImports.Inventory.BL;
using AutoImports.Inventory.Models;
using System.Net.Http;
using System.Net;
using System.IO;
using AutoImports.Inventory.API.Common;
using System.Configuration;

namespace AutoImports.Inventory.API
{
    public class InventoryController : PagingController<InventoryBL>
    {

        [HttpGet]
        [Route("api/ftpinventory", Name = "getFTPInventory")]
        public async Task<IHttpActionResult> GetFTPInventory()
        {
            //string url = "ftp://xeeded.com/CSV.csv"; ConfigurationManager.AppSettings["FTPUrl"].ToString();
            //string UserName = "Muzi5";
            //string Password = "Xeeded123@@";
            
            if(ConfigurationManager.AppSettings["IsFrazer"].ToString() == "1")
            {
                //Get data from Frazer
                string url = ConfigurationManager.AppSettings["FTPUrl"].ToString();
                string UserName = ConfigurationManager.AppSettings["FTPUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["FTPPassword"].ToString();

                DateTime dtFile = getFTPFileDateTime(url, UserName, Password);
                if (System.Web.HttpRuntime.Cache["ftpDateTime"] != null)
                {
                    DateTime dt = (DateTime)System.Web.HttpRuntime.Cache["ftpDateTime"];
                    if (dt == dtFile)
                    {
                        if (System.Web.HttpRuntime.Cache["ftpData"] != null)
                        {
                            var lst = (List<InventoryModel>)System.Web.HttpRuntime.Cache["ftpData"];
                            return Ok(lst);
                        }
                        else
                        {
                            var result1 = await ExecuteAsync(() => BL.GetInventory("l"));
                            return Ok(result1);
                        }
                    }
                }
                var result = await ExecuteAsync(() => BL.GetFTPInventory(url, UserName, Password));
                if (result != null)
                {
                    System.Web.HttpRuntime.Cache["ftpData"] = result;
                    System.Web.HttpRuntime.Cache["ftpDateTime"] = dtFile;
                }
                return Ok(result);

            }
            else
            {
                // Get data from database
                var result1 = await ExecuteAsync(() => BL.GetInventory("l"));
                return Ok(result1);
            }
        }

        [HttpGet]
        [Route("api/ftpappinventory", Name = "getFTPAppInventory")]
        public async Task<IHttpActionResult> GetFTPAppInventory()
        {
            string url = "ftp://xeeded.com/CSV.csv";
            string UserName = "Muzi5";
            string Password = "Xeeded123@@";

            DateTime dtFile = getFTPFileDateTime(url, UserName, Password);

            if (System.Web.HttpRuntime.Cache["ftpAppDateTime"] != null)
            {
                DateTime dt = (DateTime)System.Web.HttpRuntime.Cache["ftpAppDateTime"];
                if (dt == dtFile && System.Web.HttpRuntime.Cache["invAppData"] != null)
                {
                    var lst = (List<AppInventoryModel>)System.Web.HttpRuntime.Cache["invAppData"];
                    return Ok(lst);
                }
            }
            var result = await ExecuteAsync(() => BL.GetFTPAppInventory(url, UserName, Password));
            if(result != null)
            {
                System.Web.HttpRuntime.Cache["invAppData"] = result;
                System.Web.HttpRuntime.Cache["ftpAppDateTime"] = dtFile;
            }
            return Ok(result);
        }

        private DateTime getFTPFileDateTime(string url, string UserName, string Password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Credentials = new NetworkCredential(UserName, Password);
            request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return response.LastModified;
        }

        [HttpGet]
        [Route("api/inventory/{operation}", Name = "getInventory")]
        public async Task<IHttpActionResult> GetInventory(string Operation)
        {
            var result = await ExecuteAsync(() => BL.GetInventory(Operation));
            return Ok(result);
        }

        [HttpGet]
        [Route("api/inventory", Name = "getAllInventory")]
        public async Task<IHttpActionResult> GetInventory()
        {
            var result = await ExecuteAsync(() => BL.GetInventory("l"));
            return Ok(result);
        }

        [HttpGet]
        [Route("api/inventory/{inventoryId:int}", Name = "getInventoryById")]
        public async Task<IHttpActionResult> GetInventory(int inventoryId)
        {
            var result = await ExecuteAsync(() => BL.GetInventoryById(inventoryId));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/vehicles", Name = "CUDVehicles")]
        public async Task<IHttpActionResult> CUDVehicles([FromBody] InventoryModel value)
        {
            System.Web.HttpRuntime.Cache.Remove("ftpData");
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/markassold", Name = "MarkAsSold")]
        public async Task<IHttpActionResult> MarkAsSold([FromBody] InventoryModel value)
        {
            System.Web.HttpRuntime.Cache.Remove("ftpData");
            var result = await ExecuteAsync(() => BL.MarkAsSold(value));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/archiveinventory/{operation}", Name = "SetArchiveInventory")]
        public async Task<IHttpActionResult> SetArchiveInventory([FromBody] string ids, string Operation)
        {
            System.Web.HttpRuntime.Cache.Remove("ftpData");
            var result = await ExecuteAsync(() => BL.SetArchiveInventory(ids, Operation));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/upload", Name = "UploadFile")]
        public async Task<IHttpActionResult> UploadFile()
        {
            System.Web.HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
            string uploadFilePath = System.Web.Hosting.HostingEnvironment.MapPath("/Gallery/") + Path.GetFileName(Guid.NewGuid().ToString() + "_" + new System.IO.FileInfo(file.FileName).Name);

            if (file.ContentLength > 0)
            {
                file.SaveAs(uploadFilePath);
                var result = await ExecuteAsync(() => BL.CopyInventoryFromExcel(ConfigurationManager.AppSettings["ExcelConnection"].ToString().Replace("ExcelFilePath", uploadFilePath)));
                System.Web.HttpRuntime.Cache["ftpData"] = null;
                File.Delete(uploadFilePath);
                if (result)
                    return Ok("Uploaded successfully");
            }
            return InternalServerError();
        }
    }
}
