using AlexaSkillsKit.UI;
using System.Collections.Generic;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder
{
    public static class SpeechletResponseBuilderCardExtentions
    {
        public static ISpeechletResponseBuilder WithSimpleCard(this ISpeechletResponseBuilder builder,
            string title, string content) {
            return builder.WithCard(new SimpleCard {
                Content = content,
                Title = title
            });
        }

        public static ISpeechletResponseBuilder WithStandardCard(this ISpeechletResponseBuilder builder,
            string title, string text, Image image) {
            return builder.WithCard(new StandardCard {
                Text = text,
                Title = title,
                Image = image
            });
        }

        public static ISpeechletResponseBuilder WithLinkAccountCard(this ISpeechletResponseBuilder builder) {
            return builder.WithCard(new LinkAccountCard());
        }

        public static ISpeechletResponseBuilder WithAskForPermissionsConsentCard(this ISpeechletResponseBuilder builder,
            IEnumerable<string> permissions) {
            return builder.WithCard(new AskForPermissionsConsentCard {
                Permissions = permissions
            });
        }
    }
}