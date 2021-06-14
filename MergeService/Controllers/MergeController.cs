using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonthService.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MergeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class MergeController : ControllerBase
    {
        private IConfiguration Configuration;
        private int daysServiceResponse;
        private Months monthServiceResponse;

        public MergeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static readonly string[] HotCountries = new[]
        {
            "Spain", "Dubai", "India", "Turkey", "Egypt", "Thailand", "Singapore", "Australia", "Morocco", "Mauritius", "Cuba", "Mexico", "South Africa", "Canary Islands", "Italy"
        };

        private static readonly string[] ColdCountries = new[]
        {
            "Finland", "Iceland", "New Zealand", "Norway", "Alaska", "Canada", "Uruguay", "Russia"
        };


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var daysService = $"http://localhost:17829/days";
            var daysService = $"{Configuration["daysServiceURL"]}/days";
            var daysServiceResponseCall = await new HttpClient().GetStringAsync(daysService);
            daysServiceResponse = int.Parse(daysServiceResponseCall);

            //var monthService = $"http://localhost:44717/month";
            var monthService = $"{Configuration["monthServiceURL"]}/month";
            var monthServiceResponseCall = await new HttpClient().GetStringAsync(monthService);
            monthServiceResponse = (Months)Enum.Parse(typeof(Months), monthServiceResponseCall);

            //var mergedResponse = $"{monthServiceResponseCall}{daysServiceResponseCall}";
            var mergedResponse = GetResult(daysServiceResponse, monthServiceResponse);
            string response = mergedResponse + "," + monthServiceResponse.ToString() + "," + daysServiceResponseCall;
            return Ok(response);
        }

        private string GetResult(int day, Months month) 
        {
            //return a country based on the temperature of the holiday month
            //Cooler months --> return a hot country 
            //Warmer months --> return a cold country
            switch (month)
            {
                case Months.JAN:
                    return GetHotCountry();
                case Months.FEB:
                    return GetHotCountry();
                case Months.MAR:
                    return GetHotCountry();
                case Months.APR:
                    return GetHotCountry();
                case Months.MAY:
                    return GetColdCountry();
                case Months.JUN:
                    return GetColdCountry();
                case Months.JUL:
                    return GetColdCountry();
                case Months.AUG:
                    return GetColdCountry();
                case Months.SEP:
                    return GetColdCountry();
                case Months.OCT:
                    return GetHotCountry();
                case Months.NOV:
                    return GetHotCountry();
                case Months.DEC:
                    return GetHotCountry();
                default:
                    return GetHotCountry();

            }
            
        }

        private string GetHotCountry()
        {
            //get random country 
            return HotCountries[new Random().Next(0, HotCountries.Length)];
        }
        private string GetColdCountry()
        {
            //get random country
            return ColdCountries[new Random().Next(0, ColdCountries.Length)];
        }
    }
}
