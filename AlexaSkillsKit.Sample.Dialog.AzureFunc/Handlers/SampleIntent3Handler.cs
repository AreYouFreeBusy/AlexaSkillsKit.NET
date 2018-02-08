using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers
{
    public class SampleIntent3Handler : IntentHandler
    {
        public SampleIntent3Handler(ISpeechletResponseBuilder responseBuilder, ILogHelper logHelper)
            : base(responseBuilder, logHelper)
        {
        }

        public async override Task<ISpeechletResponseBuilder> HandleIntentAsync(Intent intent, IntentRequest.DialogStateEnum dialogState, Session session)
        {
            var slot = intent.Slots[SlotNames.SampleSlot1];
            var noValue = string.IsNullOrEmpty(slot.Value);
            if (!noValue)
            {
                await logHelper.Log($"User asked about slot1: {slot.Value}");
                return responseBuilder.Say($"Slot1 is {slot.Value}");
            }

            slot = intent.Slots[SlotNames.SampleSlot2];
            noValue = string.IsNullOrEmpty(slot.Value);
            if (!noValue)
            {
                await logHelper.Log($"User asked about slot2: {slot.Value}");
                return responseBuilder.Say($"Slot2 is {slot.Value}");
            }
            slot = intent.Slots[SlotNames.SampleSlot3];
            noValue = string.IsNullOrEmpty(slot.Value);
            if (!noValue)
            {
                await logHelper.Log($"User asked about slot3: {slot.Value}");
                var confirmed = slot.ConfirmationStatus.ToBool();
                if (confirmed == null) {
                    await logHelper.Log($"Slot3, asking to confirm");
                    var speech = dialogState != IntentRequest.DialogStateEnum.IN_PROGRESS ? $"Slot3 {slot.Value}. Correct?" : null;
                    return responseBuilder.DialogConfirmSlot(slot.Name, intent, speech);
                }
                var action = (bool)confirmed ? "confirmed" : "rejected";
                return responseBuilder.Say($"Slot3 {action} {slot.Value}");
            }
            return responseBuilder.Say("Sorry, what do you mean?").KeepSession();
        }
    }
}