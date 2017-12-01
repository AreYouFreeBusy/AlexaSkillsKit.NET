using AlexaSkillsKit.Directives;
using AlexaSkillsKit.UI;

namespace AlexaSkillsKit.Speechlet
{
    public interface ISpeechletResponseBuilder
    {
        SpeechletResponse Build();
        ISpeechletResponseBuilder KeepSession();
        ISpeechletResponseBuilder Say(OutputSpeech outputSpeech);
        ISpeechletResponseBuilder Say(string text);
        ISpeechletResponseBuilder SaySsml(string ssml);
        ISpeechletResponseBuilder WithCard(Card card);
        ISpeechletResponseBuilder WithDirective(Directive directive);
    }
}