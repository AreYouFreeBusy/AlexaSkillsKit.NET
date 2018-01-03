//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System.Collections.Generic;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#response-to-audioplayer-or-playbackcontroller-example-directives-only
    /// </summary>
    public class AudioPlayerResponse : ISpeechletResponse
    {
        public virtual IEnumerable<AudioPlayerDirective> Directives {
            get;
            set;
        }
    }
}