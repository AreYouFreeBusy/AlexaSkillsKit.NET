//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;

namespace AlexaSkillsKit.Speechlet
{
    public class SpeechletException : Exception
    {
        public SpeechletException() : base() {

        }

        public SpeechletException(string message) : base(message) {

        }

        public SpeechletException(string message, Exception cause) : base(message, cause) {

        }
    }
}