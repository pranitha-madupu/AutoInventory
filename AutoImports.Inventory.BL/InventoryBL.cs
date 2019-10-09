using AutoImports.Inventory.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace AutoImports.Inventory.BL
{
    public class InventoryBL : BaseBL
    {
        public List<AppInventoryModel> GetFTPAppInventory(string url, string UserName, string Password)
        {
            WebClient request = new WebClient();
            request.Credentials = new NetworkCredential(UserName, Password);
            try
            {
                string fileString = System.Text.Encoding.UTF8.GetString(request.DownloadData(url));
                if (fileString == null) return null;

                DataTable dt = GetCSVData(fileString);
                List<AppInventoryModel> lstInventory = new List<AppInventoryModel>();

                foreach (DataRow dr in dt.Rows)
                {
                    lstInventory.Add(new AppInventoryModel
                    {
                        Stock =  (dr["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Stock"].ToString())),
                        Year = (dr["Year"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Year"].ToString())),
                        Make = (dr["Make"] == DBNull.Value ? "" : dr["Make"].ToString()),
                        Model = (dr["Model"] == DBNull.Value ? "" : dr["Model"].ToString()),
                        Style = (dr["Style"] == DBNull.Value ? "" : dr["Style"].ToString()),
                        Cylinder = (dr["Cylinder"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Cylinder"].ToString())),
                        Transmission = (dr["Transmission"] == DBNull.Value ? "" : dr["Transmission"].ToString()),
                        Engine = (dr["Engine"] == DBNull.Value ? "" : dr["Engine"].ToString()),
                        DriveTrain = (dr["Drive Train"] == DBNull.Value ? "" : dr["Drive Train"].ToString()),
                        FuelType = (dr["Fuel Type"] == DBNull.Value ? "" : dr["Fuel Type"].ToString()),
                        VIN = (dr["VIN"] == DBNull.Value ? "" : dr["VIN"].ToString()),
                        Mileage = (dr["Mileage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Mileage"].ToString())),
                        Color = (dr["Color"] == DBNull.Value ? "" : dr["Color"].ToString()),
                        Color2 = (dr["Color 2"] == DBNull.Value ? "" : dr["Color 2"].ToString()),
                        InternetPrice = (dr["Internet Price"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Internet Price"].ToString())),
                        RetailPrice = (dr["Retail Price"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Retail Price"].ToString())),
                        Certified = (dr["Certified"] == DBNull.Value ? "" : dr["Certified"].ToString()),
                        VehicleType = (dr["Vehicle Type"] == DBNull.Value ? "" : dr["Vehicle Type"].ToString()),
                        Images = (dr["Photo URLs"] == DBNull.Value ? null : getImages(dr["Photo URLs"].ToString())),
                        Features = (dr["Features"] == DBNull.Value ? null : getFeatures(dr["Features"].ToString())),
                    });
                }
                return lstInventory;
            }
            catch (WebException e)
            {
                return null;
            }
        }

        public List<InventoryModel> GetFTPInventory(string url, string UserName, string Password)
        {
            WebClient request = new WebClient();
            request.Credentials = new NetworkCredential(UserName, Password);

            try
            {
                string fileString = System.Text.Encoding.UTF8.GetString(request.DownloadData(url));
                if (fileString == null) return null;

                DataTable dt = GetCSVData(fileString);

                DataTable datatable = new DataTable();
                datatable.Columns.Add("VIN", typeof(string));
                datatable.Columns.Add("Color", typeof(string));
                datatable.Columns.Add("iYear", typeof(int));
                datatable.Columns.Add("VehicleModel", typeof(string));
                //datatable.Columns.Add("Images", typeof(string));

                DataRow row = null;

                foreach (DataRow dr in dt.Rows)
                {
                    row = datatable.NewRow();
                    row["VIN"] = dr["VIN"].ToString();
                    row["Color"] = dr["Color"].ToString();
                    row["iYear"] = dr["Year"].ToString();
                    row["VehicleModel"] = dr["Model"].ToString();
                    //if(dr["Photo URLs"].ToString() != "")
                    //    row["Images"] = getImages(dr["Photo URLs"].ToString());
                    datatable.Rows.Add(row);
                }
                int result = DB.InsertData(SP.CUDFTPVehicles, "@TempVehicles", datatable);
                return  GetInventory("l");
            }
            catch 
            {
                return null;
            }
        }

        private List<string> getImages(string line)
        {
            char[] delimiters = new char[] { ',' };
            var lstImages = new List<string>();
            string[] parts = line.Split(delimiters);
            for (int i = 0; i < parts.Length; i++)
                lstImages.Add(parts[i].Trim());
            return lstImages;
        }

        private List<string> getFeatures(string line)
        {
            char[] delimiters = new char[] { ';' };
            var lstFeatures = new List<string>();
            string[] parts = line.Split(delimiters);
            for (int i = 0; i < parts.Length; i++)
                lstFeatures.Add(parts[i].Trim());
            return lstFeatures;
        }
        public List<InventoryModel> GetInventory(string Operation)
        {
            var ds = DB.ExecuteDataSet(SP.GetInventory, "@Operation", Operation);
            return ds.Tables[0].AsEnumerable().Select(r => new InventoryModel()
            {
                Id = Convert.ToInt32(r["Inventory_Id"]),
                //PurchaseDate = Convert.ToDateTime(r["PurchaseDate"]),
                PurchaseDate = DateTime.Parse(r["PurchaseDate"].ToString()),
                PurchaseFrom = (r["PurchaseFrom"] == DBNull.Value ? "" : r["PurchaseFrom"].ToString()),
                LocationFrom = (r["LocationFrom"] == DBNull.Value ? "" : r["LocationFrom"].ToString()),
                SoldTo = (r["SoldTo"] == DBNull.Value ? "" : r["SoldTo"].ToString()),
                SoldBy = (r["SoldBy"] == DBNull.Value ? "" : r["SoldBy"].ToString()),
                IsRegistration = (r["IsRegistration"] == DBNull.Value ? 0 : Convert.ToInt16(r["IsRegistration"])),
                RegistrationDone = (r["IsRegistration"] == DBNull.Value ? "No" : (Convert.ToInt16(r["IsRegistration"]) == 0 ? "No" : "Yes")),
                SaleDate = Convert.ToDateTime(r["SaleDate"]),
                VIN = (r["VIN"] == DBNull.Value ? "" : r["VIN"].ToString()),
                Color = (r["Color"] == DBNull.Value ? "" : r["Color"].ToString()),
                iYear = Convert.ToInt32(r["iYear"]),
                VehicleMake = (r["VehicleMake_Name"] == DBNull.Value ? "" : r["VehicleMake_Name"].ToString()),
                VehicleModelId = (r["VehicleModel_Id"] == DBNull.Value ? 0 : Convert.ToInt32(r["VehicleModel_Id"])),
                VehicleMakeId = (r["VehicleMake_Id"] == DBNull.Value ? 0 : Convert.ToInt32(r["VehicleMake_Id"])),
                VehicleModel = (r["VehicleModel_Name"] == DBNull.Value ? "" : r["VehicleModel_Name"].ToString()),
                Status = r["Status"].ToString(),
                Notes = (r["Notes"] == DBNull.Value ? "" : r["Notes"].ToString()),
                TypeOfSale = (r["TypeOfSale"] == DBNull.Value ? "" : r["TypeOfSale"].ToString()),
                OriginalCost = Convert.ToDouble((r["OriginalCost"] == DBNull.Value ? 0 : r["OriginalCost"])),
                TotalCost = (r["TotalCost"] == DBNull.Value ? 0 : Convert.ToDouble(r["TotalCost"])),
                SalePrice = (r["SalePrice"] == DBNull.Value ? 0 : Convert.ToDouble(r["SalePrice"])),
                AddedCost = (r["AddedCost"] == DBNull.Value ? 0 : Convert.ToDouble(r["AddedCost"])),
                Profit = (r["Profit"] == DBNull.Value ? 0 : Convert.ToDouble(r["Profit"])),
                StockInDays = Convert.ToInt32((r["StockInDays"] == DBNull.Value ? 0 : r["StockInDays"])),
                IsPaid = (r["IsPaid"] == DBNull.Value ? "" : r["IsPaid"].ToString()),
                BankId = Convert.ToInt32((r["Bank_Id"] == DBNull.Value ? 0 : r["Bank_Id"])),
                Buyer = (r["Buyer"] == DBNull.Value ? "" : r["Buyer"].ToString()),
                VTitle = (r["Title"] == DBNull.Value ? "" : r["Title"].ToString()).Trim(),
                FloorCost = (r["Floor_Cost"] == DBNull.Value ? 0 : Convert.ToDouble(r["Floor_Cost"])),
                SoldById = (r["SoldById"] == DBNull.Value ? 0 : Convert.ToInt32(r["SoldById"])),
                PurchaseFromId = (r["PurchaseFromId"] == DBNull.Value ? 0 : Convert.ToInt32(r["PurchaseFromId"])),
                BuyerId = (r["BuyerId"] == DBNull.Value ? 0 : Convert.ToInt32(r["BuyerId"])),
                DealerFee = (r["Dealer_Fee"] == DBNull.Value ? 0 : Convert.ToDouble(r["Dealer_Fee"]))
            }).ToList();
        }

        public List<InventoryModel> GetInventoryById(int InventoryId)
        {
            var ds = DB.ExecuteDataSet(SP.GetInventoryById, "@Inventory_Id", InventoryId.ToString());
            return ds.Tables[0].AsEnumerable().Select(r => new InventoryModel()
            {
                PurchaseDate = Convert.ToDateTime(r["PurchaseDate"]),
                PurchaseFrom = (r["PurchaseFrom"] == DBNull.Value ? "" : r["PurchaseFrom"].ToString()),
                LocationFrom = (r["LocationFrom"] == DBNull.Value ? "" : r["LocationFrom"].ToString()),
                SoldTo = (r["SoldTo"] == DBNull.Value ? "" : r["SoldTo"].ToString()),
                SoldBy = (r["SoldBy"] == DBNull.Value ? "" : r["SoldBy"].ToString()),
                IsRegistration = (r["IsRegistration"] == DBNull.Value ? 0 : Convert.ToInt16(r["IsRegistration"])),
                SaleDate = Convert.ToDateTime(r["SaleDate"]),
                VIN = (r["VIN"] == DBNull.Value ? "" : r["VIN"].ToString()),
                Color = (r["Color"] == DBNull.Value ? "" : r["Color"].ToString()),
                iYear = Convert.ToInt32(r["iYear"]),
                VehicleMake = r["VehicleMake_Name"].ToString(),
                VehicleModel = r["VehicleModel_Name"].ToString(),
                Status = r["Status"].ToString(),
                OriginalCost = Convert.ToDouble(r["OriginalCost"]),
                TotalCost = (r["TotalCost"] == DBNull.Value ? 0 : Convert.ToDouble(r["TotalCost"])),
                SalePrice = (r["SalePrice"] == DBNull.Value ? 0 : Convert.ToDouble(r["SalePrice"])),
                AddedCost = (r["AddedCost"] == DBNull.Value ? 0 : Convert.ToDouble(r["AddedCost"])),
                Profit = (r["Profit"] == DBNull.Value ? 0 : Convert.ToDouble(r["Profit"])),
                IsPaid = (r["IsPaid"] == DBNull.Value ? "" : r["IsPaid"].ToString()),
                BankId = Convert.ToInt32((r["Bank_Id"] == DBNull.Value ? 0 : r["Bank_Id"])),
                Buyer = (r["Buyer"] == DBNull.Value ? "" : r["Buyer"].ToString()),
                VTitle = (r["Title"] == DBNull.Value ? "" : r["Title"].ToString()).Trim(),
                FloorCost = (r["Floor_Cost"] == DBNull.Value ? 0 : Convert.ToDouble(r["Floor_Cost"])),
            }).ToList();
        }

        public int CUD(InventoryModel obj)
        {
            try
            {
                string[] paramNames = { "@Inventory_Id", "@PurchaseDate", "@VIN", "@Color", "@LocationFrom", "@iYear", "@VehicleModel_Id", "@OriginalCost","@IsPaid", "@PurchaseFrom_Id", "@Buyer_Id", "@Floor_Cost", "@Operation" };
                object[] paramValues = { obj.Id, obj.PurchaseDate, obj.VIN, obj.Color,obj.LocationFrom, obj.iYear,obj.VehicleModelId, obj.OriginalCost, obj.IsPaid,obj.PurchaseFromId, obj.BuyerId, obj.FloorCost, obj.Operation };
                int res = DB.InsertData(SP.CUDVehicles, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }

        public int MarkAsSold(InventoryModel obj)
        {
            try
            {
                string[] paramNames = { "@Inventory_Id", "@SaleDate", "@SoldTo","@SoldBy_Id", "@TypeOfSale", "@SalePrice", "@Notes", "@IsRegistration", "@IsPaid", "@Bank_Id","@Buyer_Id","@Title", "@PurchaseFrom_Id", "@Floor_Cost", "@Dealer_Fee","@Operation" };
                object[] paramValues = { obj.Id, obj.SaleDate, obj.SoldTo,obj.SoldById, obj.TypeOfSale, obj.SalePrice, obj.Notes, obj.IsRegistration, obj.IsPaid, obj.BankId, obj.BuyerId, obj.VTitle, obj.PurchaseFromId, obj.FloorCost,obj.DealerFee, obj.Operation};
                int res = DB.InsertData(SP.MarkAsSold, paramNames, paramValues);
                return res;
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
                return 0;
            }
        }

        public int SetArchiveInventory(string Ids, string Operation)
        {
            try
            {
                string[] paramNames = { "@Ids", "@Operation" };
                object[] paramValues = {Ids, Operation};
                int res = DB.InsertData(SP.SetArchiveInventory, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }

        public bool CopyInventoryFromExcel(string ExcelConnection)
        {
            return DB.CopyInventoryFromExcel(ExcelConnection);
        }

        private DataTable GetCSVData(string fileString)
        {
            DataTable dt = null;
            bool Headers = true;

            foreach (var line in fileString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Headers)
                {
                    dt = CreateTable(line);
                    Headers = false;
                }
                else
                {
                    string[] parts = System.Text.RegularExpressions.Regex.Split(line, "\",");
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < parts.Count(); i++)
                        row[dt.Columns[i].ColumnName] = parts[i].Replace("\"", "");
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        private DataTable CreateTable(string line)
        {
            string[] header = line.Split(new char[] { ',' });
            DataTable dt = new DataTable();
            for (int i = 0; i < header.Count(); i++)
                dt.Columns.Add(header[i].Replace("\"", ""));
            return dt;
        }

        //private DataTable GetTableValueParameter<T>(IList<T> source, string dbType)
        //{
        //    DataTable dt = new DataTable();
        //    DataRow dr;
        //    foreach (var item in source)
        //    {
        //        if (dt.Columns.Count == 0)
        //            CreateColumns<T>(dt, item);

        //        var props = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //        dr = dt.NewRow();
        //        foreach (var prop in props)
        //        {
        //            var cAttr = prop.GetCustomAttribute<TableValueParameterAttribute>(false);
        //            if (cAttr == null)
        //                continue;
        //            dr[cAttr.Column] = prop.GetValue(item);
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}

        //private void CreateColumns<T>(DataTable dt, T item)
        //{
        //    var cAttrs = new List<TableValueParameterAttribute>();
        //    var props = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    DataColumn dc;
        //    foreach (var prop in props)
        //    {
        //        var cAttr = prop.GetCustomAttribute<TableValueParameterAttribute>(false);
        //        if (cAttr == null)
        //            continue;
        //        cAttrs.Add(cAttr);
        //    }
        //    foreach (var cattr in cAttrs)
        //    {
        //        dc = new DataColumn(cattr.Column);
        //        dt.Columns.Add(dc);
        //    }
        //}

    }
}
