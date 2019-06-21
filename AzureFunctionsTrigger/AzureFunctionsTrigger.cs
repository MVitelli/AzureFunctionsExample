using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;

namespace Azure_Functions
{
    public static class AzureFunctionsTrigger
    {
        const string APP_KEY = "9b27903c4810f2213e3a74a8145b8d0b";
        const string BUENOS_AIRES_ID = "3433955";

        private static readonly HttpClient client = new HttpClient();

        [FunctionName("AzureFunctionsTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string cityId = req.Query["cityId"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            cityId = cityId ?? data?.cityId;

            var response = GetCurrentWeather(cityId);

            return response != null
                ? (ActionResult)new OkObjectResult(response)
                : new BadRequestObjectResult("Please pass a valid ID on the query string or in the request body");
        }

        private static WeatherDTO GetCurrentWeather(string cityId)
        {
            cityId = cityId ?? BUENOS_AIRES_ID;
            string responseString  = string.Empty;
            var queryParams = new Dictionary<string, string>(){
                    { "id", cityId },
                    { "APPID",APP_KEY }
                };
            var url = QueryHelpers.AddQueryString("https://api.openweathermap.org/data/2.5/weather", queryParams);

            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                // by calling .Result you are performing a synchronous call
                var responseContent = response.Content;

                // by calling .Result you are synchronously reading the result
                responseString = responseContent.ReadAsStringAsync().Result;
            }

            var weatherDTO = JsonConvert.DeserializeObject<WeatherDTO>(responseString,
                new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore});

            if (weatherDTO!= null)
                return WeatherConverter.ConvertToCelsius(weatherDTO);
            return weatherDTO;
        }
    }

}