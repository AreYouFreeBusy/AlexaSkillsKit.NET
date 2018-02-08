using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers
{
    public abstract class IntentHandler : IIntentHandler
    {
        protected readonly ISpeechletResponseBuilder responseBuilder;
        protected readonly ILogHelper logHelper;

        public IntentHandler(ISpeechletResponseBuilder responseBuilder, ILogHelper logHelper)
        {
            this.responseBuilder = responseBuilder;
            this.logHelper = logHelper;
        }

        public async Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session) {
            await logHelper.Log($"User {session.User.Id} in session {session.SessionId}");
            return (await HandleIntentAsync(intentRequest.Intent, intentRequest.DialogState, session)).Build();
        }

        public abstract Task<ISpeechletResponseBuilder> HandleIntentAsync(Intent intent, IntentRequest.DialogStateEnum dialogState, Session session);
    }
}