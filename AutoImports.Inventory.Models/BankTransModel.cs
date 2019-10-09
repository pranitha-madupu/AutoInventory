using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class BankTransModel : BaseModel
    {
        public DateTime BTDate { get; set; }
        public int InventoryId { get; set; }
        public string ChequeNo { get; set; }
        public string Memo { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string PayeeName { get; set; }
        public double Amount { get; set; }
        public double CurrentBalance { get; set; }
        public string PaymentStatus { get; set; }
        public string TransType { get; set; }
        public string Vin { get; set; }
    }
}
