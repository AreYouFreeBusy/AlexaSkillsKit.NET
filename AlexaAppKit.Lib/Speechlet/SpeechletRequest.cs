//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaAppKit.Speechlet
{
    public abstract class SpeechletRequest
    {
        public SpeechletRequest(string requestId) {
            RequestId = requestId;
        }

        public string RequestId {
            get;
            private set;
        }
    }
}