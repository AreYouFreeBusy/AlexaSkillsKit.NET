//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class LaunchRequest : SpeechletRequest
    {
        public LaunchRequest(JObject json) : base(json) {
        }
    }
}