using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpTestController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpTestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<string> Get()
        {
            var client = _httpClientFactory.CreateClient("httpTestClient");
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://www.baidu.com"));
            return await response.Content.ReadAsStringAsync();
        }

        [HttpGet]
        [Route("GetByMultipleParameter")]
        public async Task<string> GetByMultipleParameter() 
        {
            var client = _httpClientFactory.CreateClient("MultipleParameter");
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/common/systemArea/getMainBusinessArea"));
            return await response.Content.ReadAsStringAsync();
        }
    }
}
