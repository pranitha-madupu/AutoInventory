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
    public class ExpensesController : PagingController<ExpensesBL>
    {
        [HttpGet]
        [Route("api/expenses", Name = "getExpenses")]
        public async Task<IHttpActionResult> GetExpenses()
        {
            var result = await ExecuteAsync(() => BL.GetExpenses());
            return Ok(result);
        }

        [HttpGet]
        [Route("api/expensetypes", Name = "getExpenseTypes")]
        public async Task<IHttpActionResult> GetExpenseTypes()
        {
            var result = await ExecuteAsync(() => BL.GetExpenseTypes());
            return Ok(result);
        }

        [HttpPost]
        [Route("api/expense", Name = "CUDExpenses")]
        public async Task<IHttpActionResult> CUDExpenses([FromBody] ExpensesModel value)
        {
            var result = await ExecuteAsync(() => BL.CUDExpenses(value));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/expensetype", Name = "CUDExpenseTypes")]
        public async Task<IHttpActionResult> CUDExpenseTypes([FromBody] ExpensesModel value)
        {
            var result = await ExecuteAsync(() => BL.CUDExpenseTypes(value));
            return Ok(result);
        }
    }
}