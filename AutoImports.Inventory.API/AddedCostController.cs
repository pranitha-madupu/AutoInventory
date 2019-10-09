using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using AutoImports.Inventory.BL;
using AutoImports.Inventory.Models;

namespace AutoImports.Inventory.API
{
    public class AddedCostController : PagingController<AddedCostBL>
    {
        [HttpGet]
        [Route("api/addedcost/{inventoryId:int}", Name = "getAddedCost")]
        public async Task<IHttpActionResult> GetAddedCost(int inventoryId)
        {
            var result = await ExecuteAsync(() => BL.GetAddedCost(inventoryId));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/addedcost", Name = "CUDAddedCost")]
        public async Task<IHttpActionResult> CUDAddedCost([FromBody] AddedCostModel value)
        {
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }

    }
}