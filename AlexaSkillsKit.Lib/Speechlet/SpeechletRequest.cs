//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;

namespace AlexaSkillsKit.Speechlet
{
    public abstract class SpeechletRequest
    {
        public SpeechletRequest(string requestId, DateTime timestamp, string locale) {
            RequestId = requestId;
            Timestamp = timestamp;
            Locale = locale;
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