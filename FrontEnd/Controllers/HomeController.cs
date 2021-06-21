using FrontEnd.Interfaces;
using FrontEnd.Models;
using FrontEnd.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryWrapper repo;
        private HttpClient _client;

        public HomeController(IRepositoryWrapper repositorywrapper, HttpClient client)
        {
            repo = repositorywrapper;
            _client = client ?? new HttpClient();
        }

        public async Task<IActionResult> IndexAsync()
        {
            var mergedService = $"{Environment.GetEnvironmentVariable("mergeServiceURL")}/merge";
            var mergeResponseCall = await _client.GetStringAsync(mergedService);

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

        [ExcludeFromCodeCoverage]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
