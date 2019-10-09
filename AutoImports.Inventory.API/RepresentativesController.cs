using AutoImports.Inventory.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using AutoImports.Inventory.Models;

namespace AutoImports.Inventory.API
{
    public class RepresentativesController : PagingController<RepresentativesBL>
    {

        [HttpGet]
        [Route("api/representatives", Name = "getRepresentatives")]
        public async Task<IHttpActionResult> GetRepresentatives()
        {
            if (System.Web.HttpRuntime.Cache["Representatives"] != null)
            {
                var lst = (List<RepresentativesModel>)System.Web.HttpRuntime.Cache["Representatives"];
                return Ok(lst);
            }
            var result = await ExecuteAsync(() => BL.GetRepresentatives());
            if (result != null)
                System.Web.HttpRuntime.Cache["Representatives"] = result;
            return Ok(result);
        }

        [HttpPost]
        [Route("api/representative", Name = "CUDRepresentatives")]
        public async Task<IHttpActionResult> CUDRepresentatives([FromBody] RepresentativesModel value)
        {
            System.Web.HttpRuntime.Cache.Remove("Representatives");
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }
    }
}