using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class BanksBL : BaseBL
    {
        public List<BanksModel> GetBanks()
        {
            var ds = DB.ExecuteDataSet(SP.GetBanks);
            return ds.Tables[0].AsEnumerable().Select(r => new BanksModel()
            {
                Id = Convert.ToInt32(r["Bank_Id"]),
                BankName = r["Bank_Name"].ToString(),
                BankAddress = r["Bank_Address"].ToString(),
                CurrentBalance = Convert.ToDouble(r["Current_Balance"])
            }).ToList();
        }

        public int CUD(BanksModel obj)
        {
            try
            {
                string[] paramNames = { "@Bank_Id", "@Bank_Name", "@Bank_Address", "@Current_Balance", "@User_Id", "@Operation" };
                object[] paramValues = { obj.Id, obj.BankName, obj.BankAddress, obj.CurrentBalance, obj.UserId, obj.Operation };
                int res = DB.InsertData(SP.CUDBanks, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }
    }
}
