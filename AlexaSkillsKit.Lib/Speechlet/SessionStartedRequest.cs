//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.Speechlet
{
    public class SessionStartedRequest : SpeechletRequest
    {
        public SessionStartedRequest(string requestId, DateTime timestamp) 
            : base(requestId, timestamp) {
        }
    }
}