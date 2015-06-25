//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;

namespace AlexaSkillsKit.Speechlet
{
    public class LaunchRequest : SpeechletRequest
    {
        public LaunchRequest(string requestId, DateTime timestamp) 
            : base(requestId, timestamp) {
        }
    }
}