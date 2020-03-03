using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace AppConfigFunc
{
    public class SayHello
    {
        private readonly SampleObject _sampleObject;
        public SayHello(IOptions<SampleObject> sampleObject)
        {
            _sampleObject = sampleObject.Value;
        }

        [FunctionName("SayHello")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            return new OkObjectResult(_sampleObject);
        }
    }
}
