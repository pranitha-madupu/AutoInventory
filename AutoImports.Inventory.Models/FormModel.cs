using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class FormModel : BaseModel
    {
        public string FormName { get; set; }
        public int ParentId { get; set; }

    }
}
