using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class BanksModel : BaseModel
    {
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public double CurrentBalance { get; set; }
        public int UserId { get; set; }
    }
}
