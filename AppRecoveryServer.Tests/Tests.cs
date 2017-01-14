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
    public class Tests
    {
        [Fact(DisplayName = "Test test")]
        public async Task MyFirstIntegrationTest()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>();
            using (var host = new TestServer(webHostBuilder))
            {
                using (var client = host.CreateClient())
                {
                    var requestData = new { clientId = "Mike", email = "email", clientSecret = "secret",  };
                    var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "applicaiton/json");
                    var response = await client.PutAsync("api/logon", content);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }
    }
}
