using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppRecoveryServer.Managers;
using AppRecoveryServer.RequestBodies;

namespace AppRecoveryServer.Controllers
{
    [Route("api/[controller]")]
    public class LogonController : Controller
    {
        //POST api/logon
        //SignIn
#if !DEBUG
        [RequireHttps]
#endif
        [HttpPost]
        public IActionResult Post(
            [FromBody]SignInRequest data)
        {
            if(UsersManager.ValidateUser(data.clientId, data.clientSecret) != 0)
            {
                return Json(new { status = StatusMessages.OK });
            }
            else
            {
                this.Response.StatusCode = 401;
                return Json(new { status = StatusMessages.UserNotFound });
            }
        }

        //PUT api/logon
        //SignUp
#if !DEBUG
        [RequireHttps]
#endif
        [HttpPut]
        public IActionResult Put(
            [FromBody]SignUpRequest data)
        {
            if(UsersManager.ValidateUserNotExists(data.clientId))
            {
                UsersManager.CreateUser(data.clientId, data.clientSecret, data.email);
                return Json(new { status = StatusMessages.OK });
            }
            else
            {
                this.Response.StatusCode = 409;
                return Json(new { status = StatusMessages.UserAlreadyExists });
            }
        }
    }
}
