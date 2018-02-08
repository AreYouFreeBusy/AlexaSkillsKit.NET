using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log
{
    public class TraceWriterLogHelper: ILogHelper
    {
        private TraceWriter log;

        public TraceWriterLogHelper(TraceWriter log) {
            this.log = log;
        }

        public Task Log(string message) {

            return Task.CompletedTask;
        }
    }
}