using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MonthFunction;

namespace MergeFunction
{
    public static class MergeFunc
    {
        private static IConfiguration Configuration;
        private static int daysServiceResponse;
        private static Months monthServiceResponse;

        [Function("MergeFunc")]
        public static async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("MergeFunc");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var daysService = $"{Configuration["daysServiceURL"]}";
            var daysServiceResponseCall = await new HttpClient().GetStringAsync(daysService);
            daysServiceResponse = int.Parse(daysServiceResponseCall);

            var monthService = $"{Configuration["monthServiceURL"]}";
            var monthServiceResponseCall = await new HttpClient().GetStringAsync(monthService);
            monthServiceResponse = (Months)Enum.Parse(typeof(Months), monthServiceResponseCall);

            var mergedResponse = GetResult(daysServiceResponse, monthServiceResponse);
            string result = mergedResponse + "," + monthServiceResponse.ToString() + "," + daysServiceResponseCall;

            response.WriteString(result);

            return response;
        }

        public static readonly string[] HotCountries = new[]
        {
            "Spain", "Dubai", "India", "Turkey", "Egypt", "Thailand", "Singapore", "Australia", "Morocco", "Mauritius", "Cuba", "Mexico", "South Africa", "Canary Islands", "Italy"
        };

        public static readonly string[] ColdCountries = new[]
        {
            "Finland", "Iceland", "New Zealand", "Norway", "Alaska", "Canada", "Uruguay", "Russia"
        };

        public static string GetResult(int day, Months month)
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

        public static string GetHotCountry()
        {
            //get random country 
            return HotCountries[new Random().Next(0, HotCountries.Length)];
        }
        public static string GetColdCountry()
        {
            //get random country
            return ColdCountries[new Random().Next(0, ColdCountries.Length)];
        }
    }
}
