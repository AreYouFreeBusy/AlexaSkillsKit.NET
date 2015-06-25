//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.Speechlet
{
    public abstract class SpeechletRequest
    {
        public SpeechletRequest(string requestId, DateTime timestamp) {
            RequestId = requestId;
            Timestamp = timestamp;
        }

        public string RequestId {
            get;
            private set;
        }

        public DateTime Timestamp {
            get;
            private set;
        }
    }
}