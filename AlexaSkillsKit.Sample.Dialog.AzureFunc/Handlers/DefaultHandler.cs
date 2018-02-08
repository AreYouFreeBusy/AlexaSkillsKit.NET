using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers
{
    public class DefaultHandler : IntentHandler
    {
        private static readonly string HelpIndexAttribute = "Index";
        private static readonly string[] HelpResponces = new[]
        {
            "Say something",
            "Are you still here?",
            "What can I help you?",
            "WTF?"
        };

        public DefaultHandler(ISpeechletResponseBuilder responseBuilder, ILogHelper logHelper)
            : base(responseBuilder, logHelper)
        {
        }

        private static int GetIntAttribute(Session session, string key, int defaultValue = 0)
        {
            string value = session.Attributes.TryGetValue(key, out value) ? value : string.Empty;
            int result = int.TryParse(value, out result) ? result : 0;
            return result;
        }

        private static void SetIntAttribute(Session session, string key, int value) {
            session.Attributes[key] = $"{value}";
        }

        public async override Task<ISpeechletResponseBuilder> HandleIntentAsync(Intent intent, IntentRequest.DialogStateEnum dialogState, Session session)
        {
            await logHelper.Log($"Something unrecognized sayd");
            var index = GetIntAttribute(session, HelpIndexAttribute);
            SetIntAttribute(session, HelpIndexAttribute, (index + 1) / HelpResponces.Length);
            return responseBuilder.Say(HelpResponces[index]).KeepSession();
        }
    }
}