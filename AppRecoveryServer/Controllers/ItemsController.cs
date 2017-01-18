using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppRecoveryServer.Managers;
using AppRecoveryServer.Providers;
using AppRecoveryServer.Models;
using Newtonsoft.Json;
using AppRecoveryServer.RequestBodies;

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
            var userId = UsersManager.ValidateUser(clientId, clientSecret);
            if(userId == 0)
            {
                this.Response.StatusCode = 401;
                return JsonConvert.SerializeObject(new { status = StatusMessages.UserNotFound });
            }
            else
            {
                var items = ItemsManager.GetItems(userId);
                return String.Join(Environment.NewLine, items.Select(item => 
                  JsonConvert.SerializeObject(new { id = item.Id, caption = item.Name, description = item.Description, order = item.Sort, url = item.Url, status = "OK" })));
            }
        }

        //POST api/items
        //Post new item
#if !DEBUG
        [RequireHttps]
#endif
        [HttpPost]
        public IActionResult Post(
            [FromHeader]String clientId,
            [FromHeader]String clientSecret,
            [FromBody]InsertItemRequest data)
        {
            var userId = UsersManager.ValidateUser(clientId, clientSecret);
            if (userId == 0)
            {
                this.Response.StatusCode = 401;
                return Json(new { status = StatusMessages.UserNotFound });
            }
            else
            {
                ItemsManager.InsertItem(userId, data.caption, data.description, data.order, data.url);
                return Json(new { status = StatusMessages.OK });
            }
        }

        //PATCH api/items/1
        //Modify specified item
#if !DEBUG
        [RequireHttps]
#endif
        [HttpPatch("{id}")]
        public IActionResult Patch(int id,
            [FromHeader]String clientId,
            [FromHeader]String clientSecret,
            [FromBody]UpdateItemRequest data)
        {
            var userId = UsersManager.ValidateUser(clientId, clientSecret);
            if (userId == 0)
            {
                this.Response.StatusCode = 401;
                return Json(new { status = StatusMessages.UserNotFound });
            }
            else
            {
                if(ItemsManager.UpdateItem(id, userId, data.caption, data.description, data.order, data.url))
                {
                    return Json(new { status = StatusMessages.OK });
                }
                else
                {
                    this.Response.StatusCode = 404;
                    return Json(new { status = StatusMessages.ItemNotFound });
                }
            }
        }

        //DELETE api/items/1
        //Delete specified item
#if !DEBUG
        [RequireHttps]
#endif
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,
            [FromHeader]String clientId,
            [FromHeader]String clientSecret)
        {
            var userId = UsersManager.ValidateUser(clientId, clientSecret);
            if (userId == 0)
            {
                this.Response.StatusCode = 401;
                return Json(new { status = StatusMessages.UserNotFound });
            }
            else
            {
                if(ItemsManager.DeleteItem(id, userId))
                {
                    return Json(new { status = StatusMessages.OK });
                }
                else
                {
                    this.Response.StatusCode = 404;
                    return Json(new { status = StatusMessages.ItemNotFound });
                }
            }
        }
    }
}
