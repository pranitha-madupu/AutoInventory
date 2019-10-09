using AutoImports.Inventory.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AutoImports.Inventory.API
{
    public class FieldController : PagingController<FieldBL>
    {
        [HttpGet]
        [Route("api/fieldsall/{u}", Name = "getFieldsAll")]
        public async Task<IHttpActionResult> GetFields(string u)
        {
            var result = await ExecuteAsync(() => BL.GetFields( (u == "0" ? null : u )  ));
            return Ok(result);
        }

        [HttpGet]
        [Route("api/fields/{u}", Name = "getFields")]
        public async Task<IHttpActionResult> GetPermissions(string u)
        {
            var result = await ExecuteAsync(() => BL.GetPermissions(u));
            return Ok(result);
        }
    }
}