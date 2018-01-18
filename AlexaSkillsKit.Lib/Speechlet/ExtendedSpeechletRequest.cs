// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#request-body-parameters
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#requests
    /// https://developer.amazon.com/docs/custom-skills/playback-controller-interface-reference.html#requests
    /// </summary>
    public class ExtendedSpeechletRequest : SpeechletRequest
    {
        public ExtendedSpeechletRequest(string type, string subtype, JObject json) : base(json) {
            Type = type;
            Subtype = subtype;
        }

        public string Type {
            get;
            private set;
        }

        public string Subtype {
            get;
            private set;
        }
    }
}