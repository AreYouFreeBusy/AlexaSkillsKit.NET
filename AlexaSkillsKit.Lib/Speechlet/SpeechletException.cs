// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

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