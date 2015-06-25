//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.Speechlet
{
    public class SessionEndedRequest : SpeechletRequest
    {
        public SessionEndedRequest(string requestId, DateTime timestamp, SessionEndedRequest.ReasonEnum reason) 
            : base(requestId, timestamp) {

            Reason = reason;
        }

        public virtual SessionEndedRequest.ReasonEnum Reason {
            get;
            private set;
        }

        public enum ReasonEnum        
        {
            NONE = 0, // default in case parsing fails
            ERROR,
            USER_INITIATED,
            EXCEEDED_MAX_REPROMPTS,
        }
    }
}