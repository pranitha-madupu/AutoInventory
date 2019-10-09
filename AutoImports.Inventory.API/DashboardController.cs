using AutoImports.Inventory.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AutoImports.Inventory.API
{
    public class DashboardController : PagingController<DashboardBL>
    {
        [HttpGet]
        [Route("api/dashboard", Name = "getDashboard")]
        public async Task<IHttpActionResult> GetDashboard()
        {
            var result = await ExecuteAsync(() => BL.GetDashboard());
            return Ok(result);
        }
    }
}