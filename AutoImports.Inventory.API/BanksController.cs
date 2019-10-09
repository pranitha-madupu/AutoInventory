using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoImports.Inventory.BL;
using System.Web.Http;
using System.Threading.Tasks;
using AutoImports.Inventory.Models;

namespace AutoImports.Inventory.API
{
    public class BanksController : PagingController<BanksBL>
    {
        [HttpGet]
        [Route("api/banks", Name = "getBanks")]
        public async Task<IHttpActionResult> GetBanks()
        {
            if (System.Web.HttpRuntime.Cache["Banks"] != null)
            {
                var lst = (List<BanksModel>)System.Web.HttpRuntime.Cache["Banks"];
                return Ok(lst);
            }
            var result = await ExecuteAsync(() => BL.GetBanks());
            if (result != null)
                System.Web.HttpRuntime.Cache["Banks"] = result;
            return Ok(result);
        }

        [HttpPost]
        [Route("api/bank", Name = "CUDBanks")]
        public async Task<IHttpActionResult> CUDBanks([FromBody] BanksModel value)
        {
            System.Web.HttpRuntime.Cache.Remove("Banks");
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }
    }
}