using AutoImports.Inventory.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using AutoImports.Inventory.Models;

namespace AutoImports.Inventory.API
{
    public class VehicleMakeController : PagingController<VehicleMakeBL>
    {
        [HttpGet]
        [Route("api/vehiclemakes", Name = "getVehicleMake")]
        public async Task<IHttpActionResult> GetVehicleMake()
        {
            if (System.Web.HttpRuntime.Cache["vehicleMakes"] != null)
            {
                var lst = (List<VehicleMakeModel>)System.Web.HttpRuntime.Cache["vehicleMakes"];
                return Ok(lst);
            }
            var result = await ExecuteAsync(() => BL.GetVehicleMake());
            if (result != null)
                System.Web.HttpRuntime.Cache["vehicleMakes"] = result;
            return Ok(result);
        }

        [HttpGet]
        [Route("api/vehiclemake/{inventoryId:int}", Name = "getVehicleMakeById")]
        public async Task<IHttpActionResult> GetVehicleMake(int inventoryId)
        {
            var result = await ExecuteAsync(() => BL.GetVehicleMake(inventoryId));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/vehiclemake", Name = "CUDVehicleMake")]
        public async Task<IHttpActionResult> CUDVehicleMake([FromBody] VehicleMakeModel value)
        {
            System.Web.HttpRuntime.Cache.Remove("vehicleMakes");
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }
    }
}