using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.UI
{
    public static class SpeechletResponseBuilderExtentions
    {
        public static ISpeechletResponseBuilder WithSimpleCard(this ISpeechletResponseBuilder builder, string title, string content)
        {
            return builder.WithCard(new SimpleCard { Content = content, Title = title });
        }

        public static ISpeechletResponseBuilder WithStandardCard(this ISpeechletResponseBuilder builder, string title, string content, Image image)
        {
            return builder.WithCard(new StandardCard { Text = content, Title = title, Image = image });
        }

        public static ISpeechletResponseBuilder WithLinkAccountCard(this ISpeechletResponseBuilder builder)
        {
            return builder.WithCard(new LinkAccountCard());
        }
    }
}