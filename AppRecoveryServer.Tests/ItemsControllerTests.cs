using AppRecoveryServer.Models;
using AppRecoveryServer.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AppRecoveryServer.Tests
{
    public class ItemsControllerTests
    {
        private TestServer server = null;
        private IDataProvider dataProvider = null;
        private const String clientId = "clientId";
        private const String clientSecret = "clientSecret";
        private const String email = "email";

        public ItemsControllerTests()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>();
            server = new TestServer(webHostBuilder);

            dataProvider = DataProviderFactory.GetDataProvider();
        }

        public void Dispose()
        {
            server.Dispose();
            dataProvider = null;
        }

        private HttpRequestMessage CreateMessage(HttpMethod method, String endpointSuffix = null)
        {
            var message = new HttpRequestMessage(method, $"api/items{endpointSuffix ?? String.Empty}");
            message.Headers.Add("clientId", clientId);
            message.Headers.Add("clientSecret", clientSecret);
            return message;
        }

        private void TearUp()
        {
            TestTools.ClearTable<Items>();

            Users user = new Users();
            user.Login = clientId;
            user.Password = TestTools.HashPassword(clientSecret);
            user.Email = email;

            dataProvider.Insert(user);
        }

        [Fact(DisplayName = "GetItemsTest")]
        public async Task GetItemsTest()
        {
            TearUp();

            var description = "description";
            var name = "name";
            var sort = 123;
            var url = "url";

            Items item = new Items();
            item.Description = description;
            item.Name = name;
            item.Sort = sort;
            item.Url = url;

            dataProvider.Insert(item);

            using (var client = server.CreateClient())
            {
                var message = CreateMessage(HttpMethod.Get);
                var response = await client.SendAsync(message);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var responseDefinition = new { caption = "", description = "", order = 0, url = "" };
                var deserializedResponse = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), responseDefinition);
                Assert.Equal(name, deserializedResponse.caption);
                Assert.Equal(description, deserializedResponse.description);
                Assert.Equal(sort, deserializedResponse.order);
                Assert.Equal(url, deserializedResponse.url);
            }
        }

        [Fact(DisplayName = "InsertItemTest")]
        public async Task InsertItemTest()
        {
            TearUp();

            var description = "description";
            var name = "name";
            var sort = 123;
            var url = "url";

            using (var client = server.CreateClient())
            {
                var message = CreateMessage(HttpMethod.Post);
                var requestData = new { caption = name, description = description, order = sort, url = url };
                message.Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "applicaiton/json");
                var response = await client.SendAsync(message);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var item = dataProvider.SelectAll<Items>().Single();
                Assert.Equal(name, item.Name);
                Assert.Equal(description, item.Description);
                Assert.Equal(sort, item.Sort);
                Assert.Equal(url, item.Url);
            }
        }

        [Fact(DisplayName = "InsertItemTest")]
        public async Task UpdateItemTest()
        {
            TearUp();

            Items item = new Items();
            item.Description = "descrption";
            item.Name = "name";
            item.Sort = 123;
            item.Url = "url";

            dataProvider.Insert(item);

            var itemId = dataProvider.SelectAll<Items>().Single().Id;

            var otherName = "name2";
            var otherDescription = "description2";
            var otherSort = 456;
            var otherUrl = "otherUrl";

            using (var client = server.CreateClient())
            {
                var message = CreateMessage(new HttpMethod("PATCH"), $"/{itemId}");
                var requestData = new { caption = otherName, description = otherDescription, order = otherSort, url = otherUrl };
                message.Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "applicaiton/json");
                var response = await client.SendAsync(message);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                item = dataProvider.SelectAll<Items>().Single();
                Assert.Equal(otherName, item.Name);
                Assert.Equal(otherDescription, item.Description);
                Assert.Equal(otherSort, item.Sort);
                Assert.Equal(otherUrl, item.Url);
            }
        }

        [Fact(DisplayName = "DeleteItemTest")]
        public async Task DeleteItemTest()
        {
            TearUp();

            Items item = new Items();
            item.Description = "descrption";
            item.Name = "name";
            item.Sort = 123;
            item.Url = "url";

            dataProvider.Insert(item);

            var itemId = dataProvider.SelectAll<Items>().Single().Id;

            var description = "otherDescrption";
            var name = "otherName";
            var sort = 456;
            var url = "otherUrl";

            Items otherItem = new Items();
            item.Description = description;
            item.Name = name;
            item.Sort = sort;
            item.Url = url;

            dataProvider.Insert(item);

            using (var client = server.CreateClient())
            {
                var message = CreateMessage(new HttpMethod("DELETE"), $"/{itemId}");
                var response = await client.SendAsync(message);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var leftoverItem = dataProvider.SelectAll<Items>().Single();

                Assert.NotEqual(itemId, leftoverItem.Id);
                Assert.Equal(name, leftoverItem.Name);
                Assert.Equal(description, leftoverItem.Description);
                Assert.Equal(sort, leftoverItem.Sort);
                Assert.Equal(url, leftoverItem.Url);
            }
        }
    }
}
