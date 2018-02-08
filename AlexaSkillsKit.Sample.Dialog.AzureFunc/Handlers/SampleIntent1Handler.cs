using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers
{
    public class SampleIntent1Handler : IntentHandler
    {
        public SampleIntent1Handler(ISpeechletResponseBuilder responseBuilder, ILogHelper logHelper)
            : base(responseBuilder, logHelper)
        {
        }

        public async override Task<ISpeechletResponseBuilder> HandleIntentAsync(Intent intent, IntentRequest.DialogStateEnum dialogState, Session session) {
            var slot = intent.Slots[SlotNames.SampleSlot1];
            var noValue = string.IsNullOrEmpty(slot.Value);
            if (noValue && session.IsNew) {
                await logHelper.Log($"New request, asking for details");
                return responseBuilder
                    .Say("Something in particular?")
                    .KeepSession();
            }

            var confirmed = intent.ConfirmationStatus.ToBool();
            if (confirmed == null) {
                await logHelper.Log($"Request, asking to confirm");
                return responseBuilder.DialogConfirmIntent(intent, noValue ? "You requested anything. Correct?" : null);
            }

            var logMessage = (bool)confirmed ? "User rejected request" : "User confirmed request";
            await logHelper.Log(logMessage);
            var speech = (bool)confirmed ?
                (noValue ?
                    "Thanks. We will contact you soon" :
                    $"Thanks. We will contact you soon about {slot.Value}") :
                "What was your request?";
            return responseBuilder.Say(speech);
        }
    }
}