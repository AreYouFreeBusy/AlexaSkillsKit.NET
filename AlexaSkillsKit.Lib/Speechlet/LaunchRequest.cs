// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    public class LaunchRequest : SpeechletRequest
    {
        public LaunchRequest(JObject json) : base(json) {
        }
    }
}