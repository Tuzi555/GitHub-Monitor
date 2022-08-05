using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GitHubMonitor
{
    public static class StoreData
    {
        [FunctionName("StoreData")]

        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "GeneralDB", collectionName: "GitHubMonitor", ConnectionStringSetting = "CosmosDbConnection")] out string docs,
            ILogger log)
        {
            log.LogInformation("GitHub monitor processed an action");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            docs = requestBody;
            log.LogInformation("Successfully posted to Cosmos DB.");

            return new OkObjectResult("Successfully posted to Cosmos DB.");
        }
    }
}