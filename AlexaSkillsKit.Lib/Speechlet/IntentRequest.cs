//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using AlexaSkillsKit.Slu;

namespace AlexaSkillsKit.Speechlet
{
    public class IntentRequest : SpeechletRequest
    {
        public IntentRequest(string requestId, DateTime timestamp, Intent intent)  
            : base(requestId, timestamp) {

            Intent = intent;
        }

        public virtual Intent Intent {
            get;
            private set;
        }
    }
}