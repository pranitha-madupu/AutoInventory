using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoImports.Inventory.DBAccess;

namespace AutoImports.Inventory.BL
{
    public class BaseBL
    {
        protected DBAccess.DBAccess DB = default(DBAccess.DBAccess);
        public BaseBL()
        {
            DB = new DBAccess.DBAccess();
        }
    }
}
