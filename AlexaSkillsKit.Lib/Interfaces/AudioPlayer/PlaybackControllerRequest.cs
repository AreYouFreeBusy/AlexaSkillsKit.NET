using AlexaSkillsKit.Speechlet;
using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/playback-controller-interface-reference.html#requests
    /// </summary>
    public class PlaybackControllerRequest : ExtendedSpeechletRequest
    {
        public static readonly string TypeName = "PlaybackController";

        public PlaybackControllerRequest(string subtype, JObject json) : base(TypeName, subtype, json) {
        }
    }
}