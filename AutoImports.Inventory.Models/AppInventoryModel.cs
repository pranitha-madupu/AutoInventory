using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class AppInventoryModel 
    {
        public int Stock { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Style { get; set; }
        public int Cylinder { get; set; }
        public string Transmission { get; set; }
        public string Engine { get; set; }
        public string DriveTrain { get; set; }
        public string FuelType { get; set; }
        public string VIN { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public string Color2 { get; set; }
        public double InternetPrice { get; set; }
        public double RetailPrice { get; set; }
        public string Certified { get; set; }
        public string VehicleType { get; set; }
        public List<string> Features { get; set; }
        public List<string> Images { get; set; }
    }
}
