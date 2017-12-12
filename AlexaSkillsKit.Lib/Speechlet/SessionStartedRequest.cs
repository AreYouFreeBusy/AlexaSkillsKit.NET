//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;

namespace AlexaSkillsKit.Speechlet
{
    public class SessionStartedRequest : SpeechletRequest
    {
        public SessionStartedRequest(string requestId, DateTime timestamp, string locale)
            : base(requestId, timestamp, locale) {
        }
    }
}