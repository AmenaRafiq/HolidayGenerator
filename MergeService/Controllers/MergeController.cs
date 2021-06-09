using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MergeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MergeController : ControllerBase
    {
        private IConfiguration Configuration;
        public MergeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var daysService = $"{Configuration["daysServiceURL"]}/days";
            //var daysService = $"http://localhost:17829/days";
            var daysServiceResponseCall = await new HttpClient().GetStringAsync(daysService);

            var monthService = $"{Configuration["monthServiceURL"]}/month";
            //var monthService = $"http://localhost:44717/month";
            var monthServiceResponseCall = await new HttpClient().GetStringAsync(monthService);

            var mergedResponse = $"{monthServiceResponseCall}{daysServiceResponseCall}";
            return Ok(mergedResponse);
        }
    }
}
