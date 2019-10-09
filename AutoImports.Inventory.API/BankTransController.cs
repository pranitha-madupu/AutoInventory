using AutoImports.Inventory.BL;
using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AutoImports.Inventory.API
{
    public class BankTransController : PagingController<BankTransBL>
    {
        [HttpGet]
        [Route("api/banktrans/{bankid:int}", Name = "getBankTrans")]
        public async Task<IHttpActionResult> GetBankTrans(int bankId)
        {
            var result = await ExecuteAsync(() => BL.GetBankTrans(bankId));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/banktrans", Name = "BankTrans")]
        public async Task<IHttpActionResult> CUDBanks([FromBody] BankTransModel value)
        {
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }
    }
}