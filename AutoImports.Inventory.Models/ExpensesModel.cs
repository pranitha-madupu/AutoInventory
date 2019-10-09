using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class ExpensesModel : ExpenseTypeModel
    {
        public int ExpenseTypeId { get; set; }
        public double Amount { get; set; }
        public DateTime EDate { get; set; }
        public string Notes { get; set; }
    }
}
