using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;
using System.Collections.Generic;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder
{
    public class SpeechletResponseBuilder : ISpeechletResponseBuilder
    {
        private SpeechletResponse response = new SpeechletResponse { ShouldEndSession = true };
        private IList<Directive> directives = new List<Directive>();

        public SpeechletResponse Build() {
            if (directives.Count > 0) {
                response.Directives = directives;
            }
            return response;
        }

        public ISpeechletResponseBuilder KeepSession() {
            response.ShouldEndSession = false;
            return this;
        }

        public ISpeechletResponseBuilder NoSession() {
            response.ShouldEndSession = null;
            return this;
        }

        public ISpeechletResponseBuilder Say(OutputSpeech outputSpeech) {
            response.OutputSpeech = outputSpeech;
            return this;
        }

        public ISpeechletResponseBuilder Say(string text) {
            return Say(new PlainTextOutputSpeech() { Text = text });
        }

        public ISpeechletResponseBuilder SaySsml(string ssml) {
            return Say(new SsmlOutputSpeech() { Ssml = ssml });
        }

        public ISpeechletResponseBuilder WithCard(Card card) {
            response.Card = card;
            return this;
        }

        public ISpeechletResponseBuilder WithDirective(Directive directive) {
            directives.Add(directive);
            return this;
        }
    }
}