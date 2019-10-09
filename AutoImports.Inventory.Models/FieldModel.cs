using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    public class FieldModel : BaseModel
    {
        public string FieldName { set; get; }

        public bool Permission { set; get; }
    }
}
