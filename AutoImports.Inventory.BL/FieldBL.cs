using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class FieldBL : BaseBL
    {
        public List<FieldModel> GetFields(string UserId = null)
        {
            string[] paramNames = { "@User_Id"};
            object[] paramValues = { UserId };
            var ds = DB.ExecuteDataSet(SP.GetFields, paramNames, paramValues);
            //var ds = DB.ExecuteDataSet(SP.GetFields);
            return ds.Tables[0].AsEnumerable().Select(r => new FieldModel()
            {
                Id = Convert.ToInt32(r["Field_Id"]),
                FieldName = r["Field_Name"].ToString(),
                Permission = Convert.ToBoolean(r["Permission"])
            }).ToList();
        }

        public PermissionModel GetPermissions(string UserId)
        {
            PermissionModel perModel = new PermissionModel();
            string[] paramNames = { "@User_Id" };
            object[] paramValues = { UserId };
            var ds = DB.ExecuteDataSet(SP.GetPermissions, paramNames, paramValues);
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                switch(row["Field_Name"])
                {
                    case "VIN":
                        perModel.VIN = true;
                        break;
                    case "Make":
                        perModel.VehicleMake = true;
                        break;
                    case "Model":
                        perModel.VehicleModel = true;
                        break;
                    case "Year":
                        perModel.iYear = true;
                        break;
                    case "Color":
                        perModel.Color = true;
                        break;
                    case "Purchase Date":
                        perModel.PurchaseDate = true;
                        break;
                    case "Purchase From":
                        perModel.PurchaseFrom = true;
                        break;
                    case "Location":
                        perModel.LocationFrom = true;
                        break;
                    case "Original Cost":
                        perModel.OriginalCost = true;
                        break;
                    case "Total Cost":
                        perModel.TotalCost = true;
                        break;
                    case "Stock in Days":
                        perModel.StockInDays = true;
                        break;
                    case "Bank":
                        perModel.Bank = true;
                        break;
                    case "Edit":
                        perModel.Edit = true;
                        break;
                    case "IsPaid":
                        perModel.IsPaid = true;
                        break;
                }
            }
            return perModel;
        }
    }
}
