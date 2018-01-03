//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;

namespace AlexaSkillsKit.Speechlet
{
    public class SessionEndedRequest : SpeechletRequest
    {
        public SessionEndedRequest(string requestId, DateTime timestamp, string locale, ReasonEnum reason, Error error)
            : base(requestId, timestamp, locale) {

            Reason = reason;
            Error = error;
        }

        public virtual ReasonEnum Reason {
            get;
            private set;
        }

        public Error Error {
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