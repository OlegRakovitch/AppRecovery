using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AppRecoveryServer.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        //GET api/items
        //Get all items
#if !DEBUG
        [RequireHttps]
#endif
        [HttpGet]
        public String Get(
            [FromHeader]String clientId,
            [FromHeader]String clientSecret)
        {
            throw new NotImplementedException();
        }

        //POST api/items
        //Post new item
#if !DEBUG
        [RequireHttps]
#endif
        [HttpPost]
        public String Post(
            [FromHeader]String clientId,
            [FromHeader]String clientSecret,
            [FromForm]String caption,
            [FromForm]String description,
            [FromForm]int order,
            [FromForm]String link)
        {
            throw new NotImplementedException();
        }

        //PATCH api/items/1
        //Modify specified item
#if !DEBUG
        [RequireHttps]
#endif
        [HttpPatch("{id}")]
        public String Patch(int id,
            [FromHeader]String clientId,
            [FromHeader]String clientSecret,
            [FromForm]String caption,
            [FromForm]String description,
            [FromForm]int order,
            [FromForm]String link)
        {
            throw new NotImplementedException();
        }

        //DELETE api/items/1
        //Delete specified item
#if !DEBUG
        [RequireHttps]
#endif
        [HttpDelete("{id}")]
        public String Delete(int id,
            [FromHeader]String clientId,
            [FromHeader]String clientSecret)
        {
            throw new NotImplementedException();
        }
    }
}
