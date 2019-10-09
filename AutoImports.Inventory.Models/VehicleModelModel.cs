using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class VehicleModelModel : VehicleMakeModel
    {
        public int VehicleMakeId { get; set; }
        public string VehicleModelName { get; set; }
    }
}
