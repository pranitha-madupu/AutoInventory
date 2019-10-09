using AutoImports.Inventory.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class VehicleModelBL : BaseBL
    {
        public List<VehicleModelModel> GetVehicleModel(int VehicleMake_Id)
        {
            var ds = DB.ExecuteDataSet(SP.GetVehicleModel, "@VehicleMake_Id", VehicleMake_Id.ToString());
            return ds.Tables[0].AsEnumerable().Select(r => new VehicleModelModel()
            {
                Id = Convert.ToInt32(r["VehicleModel_Id"]),
                VehicleMakeId = VehicleMake_Id,
                VehicleModelName = r["VehicleModel_Name"].ToString(),
                VehicleMakeName = r["VehicleMake_Name"].ToString()
            }).ToList();
        }

        public List<VehicleModelModel> GetVehicleModel()
        {
            var ds = DB.ExecuteDataSet(SP.GetVehicleModel);
            return ds.Tables[0].AsEnumerable().Select(r => new VehicleModelModel()
            {
                Id = Convert.ToInt32(r["VehicleModel_Id"]),
                VehicleMakeId = Convert.ToInt32(r["VehicleMake_Id"]),
                VehicleModelName = r["VehicleModel_Name"].ToString(),
                VehicleMakeName = r["VehicleMake_Name"].ToString()
            }).ToList();
        }

        public int CUD(VehicleModelModel obj)
        {
            try
            {
                string[] paramNames = {"@VehicleModel_Id", "@VehicleMake_Id", "@VehicleModel_Name", "@Operation" };
                object[] paramValues = { obj.Id, obj.VehicleMakeId, obj.VehicleModelName, obj.Operation };
                int res = DB.InsertData(SP.CUDVehicleModel, paramNames, paramValues);
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
