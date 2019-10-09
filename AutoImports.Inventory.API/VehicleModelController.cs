using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using AutoImports.Inventory.BL;
using AutoImports.Inventory.Models;

namespace AutoImports.Inventory.API
{
    public class VehicleModelController : PagingController<VehicleModelBL>
    {
        [HttpGet]
        [Route("api/vehiclemodel/{vehicleMakeId:int}", Name = "getVehicleModel")]
        public async Task<IHttpActionResult> GetVehicleModel(int vehicleMakeId)
        {
            var result = await ExecuteAsync(() => BL.GetVehicleModel(vehicleMakeId));
            return Ok(result);
        }

        [HttpGet]
        [Route("api/vehiclemodels", Name = "getAllVehicleModel")]
        public async Task<IHttpActionResult> GetAllVehicleModel()
        {
            if (System.Web.HttpRuntime.Cache["vehicleModels"] != null)
            {
                var lst = (List<VehicleModelModel>)System.Web.HttpRuntime.Cache["vehicleModels"];
                return Ok(lst);
            }
            var result = await ExecuteAsync(() => BL.GetVehicleModel());
            if (result != null)
                System.Web.HttpRuntime.Cache["vehicleModels"] = result;
            return Ok(result);
        }

        [HttpPost]
        [Route("api/vehiclemodel", Name = "CUDVehicleModel")]
        public async Task<IHttpActionResult> CUDVehicleModel([FromBody] VehicleModelModel value)
        {
            System.Web.HttpRuntime.Cache.Remove("vehicleModels");
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }
    }
}