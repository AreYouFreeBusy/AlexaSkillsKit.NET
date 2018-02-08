using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc
{
    public static class AlexaHttpTrigger
    {
        private static SampleSkillFactory appFactory = new SampleSkillFactory();

        [FunctionName("AlexaHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var logHelper = appFactory.CreateLogHelper(log);
            await logHelper.Log("HTTP trigger function recieved a request.");
            await logHelper.Log($"Request: {await req.Content.ReadAsStringAsync()}");

            var app = appFactory.CreateSpeechletApp(logHelper);
            var response = await app.GetResponseAsync(req);

            await logHelper.Log($"Response: {await response.Content.ReadAsStringAsync()}");
            return response;
        }
    }
}