using AutoImports.Inventory.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class VehicleMakeBL : BaseBL
    {
        public List<VehicleMakeModel> GetVehicleMake()
        {
            var ds = DB.ExecuteDataSet(SP.GetVehicleMake, "@VehicleMake_Id", null);
            return ds.Tables[0].AsEnumerable().Select(r => new VehicleMakeModel()
            {
                Id = Convert.ToInt32(r["VehicleMake_Id"]),
                VehicleMakeName = r["VehicleMake_Name"].ToString()
            }).ToList();
        }

        public List<VehicleMakeModel> GetVehicleMake(int VehicleMake_Id)
        {
            var ds = DB.ExecuteDataSet(SP.GetVehicleMake, "@VehicleMake_Id", VehicleMake_Id.ToString());
            return ds.Tables[0].AsEnumerable().Select(r => new VehicleMakeModel()
            {
                Id = Convert.ToInt32(r["VehicleMake_Id"]),
                VehicleMakeName = r["VehicleMake_Name"].ToString()
            }).ToList();
        }

        public int CUD(VehicleMakeModel obj)
        {
            try
            {
                string[] paramNames = { "@VehicleMake_Id", "@VehicleMake_Name", "@Operation" };
                object[] paramValues = { obj.Id, obj.VehicleMakeName,  obj.Operation };
                int res = DB.InsertData(SP.CUDVehicleMake, paramNames, paramValues);
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
