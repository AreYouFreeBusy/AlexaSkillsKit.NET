// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Helpers;
using Newtonsoft.Json.Linq;
using System;

namespace AlexaSkillsKit.Speechlet
{
    public abstract class SpeechletRequest
    {
        protected SpeechletRequest(JObject json) {
            RequestId = json.Value<string>("requestId");
            Timestamp = DateTimeHelpers.FromAlexaTimestamp(json);
            Locale = json.Value<string>("locale");
        }

        protected SpeechletRequest(SpeechletRequest other) {
            RequestId = other.RequestId;
            Timestamp = other.Timestamp;
            Locale = other.Locale;
        }

        public string RequestId {
            get;
            private set;
        }

        public DateTime Timestamp {
            get;
            private set;
        }

        public string Locale {
            get;
            private set;
        }
    }
}