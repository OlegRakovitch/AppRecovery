﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public String Post(
            [FromForm]String clientId,
            [FromForm]String clientSecret)
        {
            throw new NotImplementedException();
        }

        //PUT api/logon
        //SignUp
#if !DEBUG
        [RequireHttps]
#endif
        [HttpPut]
        public String Put(
            [FromForm]String clientId,
            [FromForm]String email,
            [FromForm]String clientSecret)
        {
            throw new NotImplementedException();
        }
    }
}
