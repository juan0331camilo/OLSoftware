using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OLSoftware.Function.DTOs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OLSoftware.Function.Functions
{
    public static class ReportFunction
    {
        [FunctionName("ReportFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var client = new RestClient("https://olsoftware-api.azurewebsites.net/");
            var request = new RestRequest("api/Projects/GetReport", Method.GET);
            var response = client.Execute(request);

            var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(response.Content);
            var reportDTO = JsonConvert.DeserializeObject<List<ReportDTO>>(responseDTO.Data.ToString());

            MemoryStream ms = new MemoryStream();
            TextWriter tw = new StreamWriter(ms);

            foreach (var item in reportDTO)            
                tw.WriteLine(item.CustomerName + "|" + item.CustomerTelephone);
            tw.Close();

            var bytes = ms.ToArray();
            return new OkObjectResult(new { fileBase64Str = Convert.ToBase64String(bytes) });
        }
    }
}
