using System;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/playback-controller-interface-reference.html#requests
    /// </summary>
    public class PlaybackControllerRequest : ExtendedSpeechletRequest
    {
        public PlaybackControllerRequest(string requestId, DateTime timestamp, string locale, string subtype)
            : base(requestId, timestamp, locale, subtype) {
        }
    }
}