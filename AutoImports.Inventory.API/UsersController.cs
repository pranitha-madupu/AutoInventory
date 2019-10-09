using AutoImports.Inventory.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using AutoImports.Inventory.Models;

namespace AutoImports.Inventory.API
{
    public class UsersController : PagingController<UsersBL>
    {
        [HttpGet]
        [Route("api/users", Name = "getUsers")]
        public async Task<IHttpActionResult> GetUsers()
        {
            var result = await ExecuteAsync(() => BL.GetUsers());
            return Ok(result);
        }

        [HttpGet]
        [Route("api/appusers", Name = "getAppUsers")]
        public async Task<IHttpActionResult> GetAppUsers()
        {
            var result = await ExecuteAsync(() => BL.GetAppUsers());
            return Ok(result);
        }

        [HttpGet]
        [Route("api/appappointments", Name = "getAppAppointments")]
        public async Task<IHttpActionResult> GetAppAppointments()
        {
            var result = await ExecuteAsync(() => BL.GetAppAppointments());
            return Ok(result);
        }

        [HttpGet]
        [Route("api/validateuser/{u}/{p}", Name = "validateUser")]
        public async Task<IHttpActionResult> ValidateUser(string u, string p)
        {
            var result = await ExecuteAsync(() => BL.ValidateUser(u,p));
            return Ok(result);
        }

        [HttpGet]
        [Route("api/validateappuser/{u}/{p}", Name = "validateAppUser")]
        public async Task<IHttpActionResult> ValidateAppUser(string u, string p)
        {
            var result = await ExecuteAsync(() => BL.ValidateAppUser(u, p));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/user", Name = "CUDUsers")]
        public async Task<IHttpActionResult> CUDUsers([FromBody] UsersModel value)
        {
            var result = await ExecuteAsync(() => BL.CUD(value));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/appuser", Name = "CUDAppUsers")]
        public async Task<IHttpActionResult> CUDAppUsers([FromBody] UsersModel value)
        {
            var result = await ExecuteAsync(() => BL.CUDAppUser(value));
            return Ok(result);
        }

        [HttpPost]
        [Route("api/appappointment", Name = "CUDAppAppointment")]
        public async Task<IHttpActionResult> CUDAppAppointment([FromBody] UsersModel value)
        {
            var result = await ExecuteAsync(() => BL.CUDAppAppointment(value));
            return Ok(result);
        }
    }
}