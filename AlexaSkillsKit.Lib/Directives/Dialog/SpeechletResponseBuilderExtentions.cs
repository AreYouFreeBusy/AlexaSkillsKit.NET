using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.Slu;

namespace AlexaSkillsKit.Directives.Dialog
{
    public static class SpeechletResponseBuilderExtentions
    {
        public static ISpeechletResponseBuilder WithDialogDelegateDirective(this ISpeechletResponseBuilder builder, Intent updatedIntent)
        {
            return builder.KeepSession().WithDirective(new DialogDelegateDirective { UpdatedIntent = updatedIntent });
        }

        public static ISpeechletResponseBuilder WithDialogElicitSlotDirective(this ISpeechletResponseBuilder builder, Intent updatedIntent, string slotName)
        {
            return builder.KeepSession().WithDirective(new DialogElicitSlotDirective { UpdatedIntent = updatedIntent, SlotToElicit = slotName });
        }

        public static ISpeechletResponseBuilder WithDialogConfirmSlotDirective(this ISpeechletResponseBuilder builder, Intent updatedIntent, string slotName)
        {
            return builder.KeepSession().WithDirective(new DialogConfirmSlotDirective { UpdatedIntent = updatedIntent, SlotToConfirm = slotName });
        }

        public static ISpeechletResponseBuilder WithDialogConfirmIntentDirective(this ISpeechletResponseBuilder builder, Intent updatedIntent)
        {
            return builder.KeepSession().WithDirective(new DialogConfirmIntentDirective { UpdatedIntent = updatedIntent });
        }
    }
}