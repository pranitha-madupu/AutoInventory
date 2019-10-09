using AutoImports.Inventory.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImports.Inventory.BL
{
    public class BookmarksBL : BaseBL
    {
        public List<BookmarksModel> GetBookmarks()
        {
            var ds = DB.ExecuteDataSet(SP.GetBookmarks);
            return ds.Tables[0].AsEnumerable().Select(r => new BookmarksModel()
            {
                Id = Convert.ToInt32(r["Bookmark_Id"]),
                Name = r["Name"].ToString(),
                Description = (r["Description"] == DBNull.Value ? "" : r["Description"].ToString())
            }).ToList();
        }

        public int CUD(BookmarksModel obj)
        {
            try
            {
                string[] paramNames = { "@Bookmark_Id", "@Name", "@Description", "@Operation" };
                object[] paramValues = { obj.Id, obj.Name, obj.Description, obj.Operation };
                int res = DB.InsertData(SP.CUDBookmarks, paramNames, paramValues);
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
