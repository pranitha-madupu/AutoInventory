using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class RepresentativesBL : BaseBL
    {
        public List<RepresentativesModel> GetRepresentatives()
        {
            var ds = DB.ExecuteDataSet(SP.GetRepresentatives);
            return ds.Tables[0].AsEnumerable().Select(r => new RepresentativesModel()
            {
                Id = Convert.ToInt32(r["Representative_Id"]),
                FirstName = r["Representative_FirstName"].ToString(),
                LastName = r["Representative_LastName"].ToString(),
                Mobile = r["Representative_Mobile"].ToString(),
                Email= r["Representative_Email"].ToString(),
                RType = r["Representative_Type"].ToString(),
            }).ToList();
        }

        public int CUD(RepresentativesModel obj)
        {
            try
            {
                string[] paramNames = { "@Representative_Id", "@Representative_FirstName", "@Representative_LastName", "@Representative_Mobile", "@Representative_Email", "@Representative_Type", "@Operation" };
                object[] paramValues = { obj.Id, obj.FirstName, obj.LastName, obj.Mobile, obj.Email, obj.RType, obj.Operation };
                int res = DB.InsertData(SP.CUDRepresentatives, paramNames, paramValues);
                return res;
            }
            catch
            {
                return 0;
            }
        }
    }
}
