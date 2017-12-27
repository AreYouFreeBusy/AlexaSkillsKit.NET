using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.AudioPlayer.Directives
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