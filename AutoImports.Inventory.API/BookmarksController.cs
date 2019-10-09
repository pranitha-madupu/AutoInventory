using AutoImports.Inventory.BL;
using AutoImports.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AutoImports.Inventory.API
{
    public class BookmarksController : PagingController<BookmarksBL>
    {
        [HttpGet]
        [Route("api/bookmarks", Name = "getBookmarks")]
        public async Task<IHttpActionResult> GetBookmarks()
        {
            var result = await ExecuteAsync(() => BL.GetBookmarks());
            return Ok(result);
        }

        [HttpPost]
        [Route("api/cudbookmark", Name = "CUDBookmarks")]
        public async Task<IHttpActionResult> CUDBookmarks([FromBody] BookmarksModel value)
        {
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }
    }
}