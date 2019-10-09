using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class PermissionModel
    {
        public bool VIN { get; set; }
        public bool VehicleModel { get; set; }
        public bool VehicleMake { get; set; }
        public bool iYear { get; set; }
        public bool Color { get; set; }
        public bool PurchaseDate { get; set; }
        public bool PurchaseFrom { get; set; }
        public bool LocationFrom { get; set; }
        public bool OriginalCost { get; set; }
        public bool TotalCost { get; set; }
        public bool StockInDays { get; set; }
        public bool Bank { get; set; }
        public bool Edit { get; set; }
        public bool IsPaid { get; set; }
    }
}
