using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class AddedCostModel : BaseModel
    {
        public int InventoryId { get; set; }
        public DateTime ADate { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
