//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using AlexaSkillsKit.Slu;

namespace AlexaSkillsKit.Speechlet
{
    public class IntentRequest : SpeechletRequest
    {
        public IntentRequest(string requestId, DateTime timestamp, string locale, Intent intent, DialogStateEnum dialogState)  
            : base(requestId, timestamp, locale) {

            Intent = intent;
            DialogState = dialogState;
        }

        public virtual Intent Intent {
            get;
            private set;
        }

        public virtual DialogStateEnum DialogState
        {
            get;
            private set;
        }

        public enum DialogStateEnum
        {
            NONE = 0, // default in case parsing fails
            STARTED,
            IN_PROGRESS,
            COMPLETED
        }
    }
}