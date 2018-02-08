using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.UI;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder
{
    public interface ISpeechletResponseBuilder
    {
        SpeechletResponse Build();
        ISpeechletResponseBuilder KeepSession();
        ISpeechletResponseBuilder NoSession();
        ISpeechletResponseBuilder Say(OutputSpeech outputSpeech);
        ISpeechletResponseBuilder Say(string text);
        ISpeechletResponseBuilder SaySsml(string ssml);
        ISpeechletResponseBuilder WithCard(Card card);
        ISpeechletResponseBuilder WithDirective(Directive directive);
    }
}