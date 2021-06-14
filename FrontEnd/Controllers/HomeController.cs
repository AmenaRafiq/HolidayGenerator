using FrontEnd.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration;
        //private IRepositoryWrapper repo;

        public HomeController(IConfiguration configuration) //IRepositoryWrapper repositorywrapper)
        {
            Configuration = configuration;
            //repo = repositorywrapper;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var mergedService = $"{Configuration["mergeServiceURL"]}/merge";
            var mergeResponseCall = await new HttpClient().GetStringAsync(mergedService);

            String[] responseArray = mergeResponseCall.ToString().Split(",");

            ViewBag.responseCall = responseArray[2];
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
