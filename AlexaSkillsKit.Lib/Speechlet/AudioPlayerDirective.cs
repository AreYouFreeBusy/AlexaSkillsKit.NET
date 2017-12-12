namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#directives
    /// </summary>
    public class AudioPlayerDirective : Directive
    {
        public AudioPlayerDirective(string subtype) : base($"AudioPlayer.{subtype}") {

        }
    }
}