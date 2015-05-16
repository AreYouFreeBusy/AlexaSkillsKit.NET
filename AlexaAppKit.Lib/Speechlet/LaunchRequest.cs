//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;

namespace AlexaAppKit.Speechlet
{
    public class LaunchRequest : SpeechletRequest
    {
        public LaunchRequest(string requestId) : base(requestId) {
        }
    }
}