// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Authentication;
using System;

namespace AlexaSkillsKit.Speechlet
{
    public class SpeechletValidationException : SpeechletException
    {
        public SpeechletRequestValidationResult ValidationResult { get; }

        public SpeechletValidationException(SpeechletRequestValidationResult validationResult) : base() {
            ValidationResult = validationResult;
        }

        public SpeechletValidationException(SpeechletRequestValidationResult validationResult, string message) : base(message) {
            ValidationResult = validationResult;
        }

        public SpeechletValidationException(SpeechletRequestValidationResult validationResult, string message, Exception cause) : base(message, cause) {
            ValidationResult = validationResult;
        }
    }
}