// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#system-exceptionencountered
    /// </summary>
    public class SystemExceptionEncounteredRequest : SystemRequest
    {
        public SystemExceptionEncounteredRequest(string subtype, JObject json) : base(subtype, json) {
            Error = Error.FromJson(json.Value<JObject>("error"));
            Cause = Cause.FromJson(json.Value<JObject>("cause"));
        }

        public Error Error {
            get;
            private set;
        }

        public Cause Cause {
            get;
            private set;
        }
    }
}