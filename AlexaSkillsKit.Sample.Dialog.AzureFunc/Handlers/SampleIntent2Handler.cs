using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers
{
    public class SampleIntent2Handler : DefaultHandler
    {
        public SampleIntent2Handler(ISpeechletResponseBuilder responseBuilder, ILogHelper logHelper)
            : base(responseBuilder, logHelper)
        {
        }

        public async override Task<ISpeechletResponseBuilder> HandleIntentAsync(Intent intent, IntentRequest.DialogStateEnum dialogState, Session session)
        {
            var slot = intent.Slots[SlotNames.SampleSlot1];
            var noValue = string.IsNullOrEmpty(slot.Value);
            if (!session.IsNew && !noValue)
            {
                await logHelper.Log($"Details: {slot.Value}");
                return responseBuilder.Say("Thanks. Need something else?");
            }
            return await base.HandleIntentAsync(intent, dialogState, session);
        }
    }
}