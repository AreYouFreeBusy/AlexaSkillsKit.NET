using AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Speechlet;
using Microsoft.Azure.WebJobs.Host;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc
{
    public class SampleSkillFactory
    {
        public ILogHelper CreateLogHelper(TraceWriter log) {
            return new TraceWriterLogHelper(log);
        }

        public SpeechletBase CreateSpeechletApp(ILogHelper logHelper)
        {
            var responseBuilder = new SpeechletResponseBuilder();
            var app = new SampleSkill(logHelper);
            app.Register(IntentNames.SampleIntent1, new SampleIntent1Handler(responseBuilder, logHelper));
            app.Register(IntentNames.SampleIntent2, new SampleIntent2Handler(responseBuilder, logHelper));
            app.Register(IntentNames.SampleIntent3, new SampleIntent3Handler(responseBuilder, logHelper));
            app.RegisterDefault(new DefaultHandler(responseBuilder, logHelper));
            return app;
        }
    }
}