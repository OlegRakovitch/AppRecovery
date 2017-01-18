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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AppRecoveryServer.Tests
{
    public class LogonControllerTests : IDisposable
    {
        private TestServer server = null;
        private IDataProvider dataProvider = null;

        public LogonControllerTests()
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

        private void TearUp()
        {
            TestTools.ClearTable<Users>();
        }

        private HttpRequestMessage CreateMessage(HttpMethod method)
        {
            var message = new HttpRequestMessage(method, $"api/logon");
            return message;
        }

        private void SetContent(HttpRequestMessage message, object requestData)
        {
            message.Content = new StringContent(JsonConvert.SerializeObject(requestData));
            message.Content.Headers.Remove("Content-Type");
            message.Content.Headers.Add("Content-Type", "application/json");
        }

        [Fact(DisplayName = "SignInTest")]
        public async Task SignInTest()
        {
            TearUp();

            var login = "testlogin";
            var password = "testpassword";
            var email = "test@email.com";

            Users user = new Users();
            user.Login = login;
            user.Password = TestTools.HashPassword(password);
            user.Email = email;

            dataProvider.Insert(user);

            using (var client = server.CreateClient())
            {
                var message = CreateMessage(HttpMethod.Post);
                SetContent(message, new { clientId = login, clientSecret = password });
                var response = await client.SendAsync(message);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact(DisplayName = "SignUpTest")]
        public async Task SignUpTest()
        {
            TearUp();

            var login = "testlogin";
            var password = "testpassword";
            var email = "test@email.com";

            using (var client = server.CreateClient())
            {
                var message = CreateMessage(HttpMethod.Put);
                SetContent(message, new { clientId = login, email = email, clientSecret = password });
                var response = await client.SendAsync(message);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var user = dataProvider.SelectAll<Users>().Single();
                Assert.Equal(login, user.Login);
                Assert.Equal(TestTools.HashPassword(password), user.Password);
                Assert.Equal(email, user.Email);
            }
        }
    }
}
