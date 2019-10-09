using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class DashboardModel : BaseModel
    {
        public string Title { get; set; }

        public string Status { get; set; }

        public double Result { get; set; }
    }
}
