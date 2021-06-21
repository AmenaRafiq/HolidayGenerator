using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DaysFunction
{
    public static class DaysFunc
    {
        [Function("DaysFunc")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("DaysFunc");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //instantiate a random number generator
            var random = new Random();
            //get random integer between 0 and 10 as there are 11 values in the Days array
            var daysValue = random.Next(0, 10);
            //return the corresponding value in the array
            var days = Days[daysValue].ToString();

            response.WriteString(days);

            return response;
        }

        //array holding various lengths of a holiday
        public static readonly int[] Days = new[]
        {
            2,3,4,5,6,7,10,14,21,28,30
        };
    }
}
