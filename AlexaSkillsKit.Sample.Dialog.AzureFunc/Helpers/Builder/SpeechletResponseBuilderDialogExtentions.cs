using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Interfaces.Dialog.Directives;
using System;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder
{
    public static class SpeechletResponseBuilderDialogExtentions
    {
        private static ISpeechletResponseBuilder WithDialogDirective<T>(this ISpeechletResponseBuilder builder, Intent updatedIntent,
            Action<T> initialize = null) where T: DialogDirective, new()
        {
            var directive = new T { UpdatedIntent = updatedIntent };
            initialize?.Invoke(directive);
            return builder.KeepSession().WithDirective(directive);
        }

        public static ISpeechletResponseBuilder DialogDelegate(this ISpeechletResponseBuilder builder, Intent updatedIntent = null) {
            return builder.WithDialogDirective<DialogDelegateDirective>(updatedIntent);
        }

        public static ISpeechletResponseBuilder DialogElicitSlot(this ISpeechletResponseBuilder builder, string slotName,
            Intent updatedIntent = null, string speech = null) {
            return string.IsNullOrEmpty(speech) ?
                builder.DialogDelegate(updatedIntent) :
                builder.WithDialogDirective<DialogElicitSlotDirective>(updatedIntent, x => x.SlotToElicit = slotName).Say(speech);
        }

        public static ISpeechletResponseBuilder DialogConfirmSlot(this ISpeechletResponseBuilder builder, string slotName,
            Intent updatedIntent = null, string speech = null) {
            return string.IsNullOrEmpty(speech) ?
                builder.DialogDelegate(updatedIntent) :
                builder.WithDialogDirective<DialogConfirmSlotDirective>(updatedIntent, x => x.SlotToConfirm = slotName).Say(speech);
        }

        public static ISpeechletResponseBuilder DialogConfirmIntent(this ISpeechletResponseBuilder builder,
            Intent updatedIntent = null, string speech = null) {
            return string.IsNullOrEmpty(speech) ?
                builder.DialogDelegate(updatedIntent) :
                builder.WithDialogDirective<DialogConfirmIntentDirective>(updatedIntent).Say(speech);
        }
    }
}