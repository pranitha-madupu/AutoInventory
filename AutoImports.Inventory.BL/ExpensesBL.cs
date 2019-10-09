using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class ExpensesBL : BaseBL
    {
        public List<ExpensesModel> GetExpenses()
        {
            var ds = DB.ExecuteDataSet(SP.GetExpenses);
            return ds.Tables[0].AsEnumerable().Select(r => new ExpensesModel()
            {
                Id = Convert.ToInt32(r["Expense_Id"]),
                EDate = Convert.ToDateTime(r["EDate"]),
                ExpenseTypeId = Convert.ToInt32(r["ExpenseType_Id"]),
                ExpenseTypeName = r["ExpenseType_Name"].ToString(),
                Notes = (r["Notes"] == DBNull.Value ? "" : r["Notes"].ToString()),
                Amount = Convert.ToDouble(r["Amount"])
            }).ToList();
        }

        public List<ExpenseTypeModel> GetExpenseTypes()
        {
            var ds = DB.ExecuteDataSet(SP.GetExpenseTypes);
            return ds.Tables[0].AsEnumerable().Select(r => new ExpenseTypeModel()
            {
                Id = Convert.ToInt32(r["ExpenseType_Id"]),
                ExpenseTypeName = r["ExpenseType_Name"].ToString()
            }).ToList();
        }

        public int CUDExpenses(ExpensesModel obj)
        {
            try
            {
                string[] paramNames = { "@Expense_Id", "@ExpenseType_Id", "@Amount", "@Notes","@EDate","@Operation" };
                object[] paramValues = { obj.Id, obj.ExpenseTypeId, obj.Amount, obj.Notes, obj.EDate, obj.Operation };
                int res = DB.InsertData(SP.CUDExpenses, paramNames, paramValues);
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int CUDExpenseTypes(ExpenseTypeModel obj)
        {
            try
            {
                string[] paramNames = {"@ExpenseType_Id", "@ExpenseType_Name", "@Operation" };
                object[] paramValues = { obj.Id, obj.ExpenseTypeName, obj.Operation };
                int res = DB.InsertData(SP.CUDExpenseTypes, paramNames, paramValues);
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
