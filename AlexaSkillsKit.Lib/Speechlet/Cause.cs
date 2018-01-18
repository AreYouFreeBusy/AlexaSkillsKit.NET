// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#system-exceptionencountered
    /// </summary>
    public class Cause
    {
        public static Cause FromJson(JObject json) {
            if (json == null) return null;

            return new Cause {
                RequestId = json.Value<string>("requestId")
            };
        }

        public string RequestId {
            get;
            private set;
        }
    }
}