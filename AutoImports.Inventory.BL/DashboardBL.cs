using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class DashboardBL : BaseBL
    {
        public List<DashboardModel> GetDashboard()
        {
            var ds = DB.ExecuteDataSet(SP.GetDashboard);
            return ds.Tables[0].AsEnumerable().Select(r => new DashboardModel()
            {
                Title = r["Title"].ToString(),
                Status = r["Status"].ToString(),
                Result = (r["Result"] == DBNull.Value ? 0 : Convert.ToDouble(r["Result"]))
            }).ToList();
        }
    }
}
