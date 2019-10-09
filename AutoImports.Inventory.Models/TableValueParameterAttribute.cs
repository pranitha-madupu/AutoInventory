using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableValueParameterAttribute : Attribute
    {
        public TableValueParameterAttribute(string column)
        {
            this.Column = column;
        }
        public string Column { set; get; }
    }
}
