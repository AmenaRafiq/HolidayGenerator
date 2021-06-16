using FrontEnd.Interfaces;
using FrontEnd.Models;
using FrontEnd.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration;
        private IRepositoryWrapper repo;

        public HomeController(IConfiguration configuration, IRepositoryWrapper repositorywrapper)
        {
            Configuration = configuration;
            repo = repositorywrapper;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var mergedService = $"{Configuration["mergeServiceURL"]}/merge";
            var mergeResponseCall = await new HttpClient().GetStringAsync(mergedService);

            //split the response into appropriate variables
            String[] responseArray = mergeResponseCall.ToString().Split(",");
            string destination = responseArray[0];
            string month = responseArray[1];
            string days = responseArray[2];

            ViewBag.destination = destination;
            ViewBag.month = month;
            ViewBag.days = days;

            //create an entry in the database
            StoreEntryInDatabase(destination, month, days);

            return View();
        }

        public void StoreEntryInDatabase(string destination, string month, string days)
        {
            //create an entry in the results table
            var result = new Result
            {
                Period = month + days,
                Country = destination,
            };
            repo.Results.Create(result);
            repo.Save();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
