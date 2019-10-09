using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class FormAccessBL : BaseBL
    {
        public List<FormModel> GetForms(string UserId = null)
        {
            string[] paramNames = { "@User_Id" };
            object[] paramValues = { UserId };
            var ds = DB.ExecuteDataSet(SP.GetForms, paramNames, paramValues);
            return ds.Tables[0].AsEnumerable().Select(r => new FormModel()
            {
                Id = Convert.ToInt32(r["Form_Id"]),
                FormName = r["Form_Name"].ToString(),
                ParentId = Convert.ToInt32(r["Parent_Id"])
            }).ToList();
        }
    }
}
