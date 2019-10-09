using AutoImports.Inventory.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class AddedCostBL : BaseBL
    {
        public List<AddedCostModel> GetAddedCost(int Inventory_Id)
        {
            var ds = DB.ExecuteDataSet(SP.GetAddedCost, "@Inventory_Id", Inventory_Id.ToString());
            return ds.Tables[0].AsEnumerable().Select(r => new AddedCostModel()
            {
                Id = Convert.ToInt32(r["AddedCost_Id"]),
                InventoryId = Inventory_Id,
                Description = r["Description"].ToString(),
                Price = Convert.ToDouble(r["Price"])
            }).ToList();
        }

        public int CUD(AddedCostModel obj)
        {
            try
            {
                string[] paramNames = { "@Inventory_Id", "@AddedCost_Id", "@Description", "@Price", "@Operation" };
                object[] paramValues = { obj.InventoryId, obj.Id, obj.Description, obj.Price, obj.Operation };
                int res = DB.InsertData(SP.CUDAddedCost, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }

    }
}
