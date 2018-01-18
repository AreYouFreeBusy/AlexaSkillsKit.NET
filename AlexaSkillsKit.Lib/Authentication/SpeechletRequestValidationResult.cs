// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;

namespace AlexaSkillsKit.Authentication
{
    [Flags]
    public enum SpeechletRequestValidationResult
    {
        OK = 0,
        NoSignatureHeader = 1,
        NoCertHeader = 2,
        InvalidSignature = 4,
        InvalidTimestamp = 8,
        InvalidJson = 16,
        InvalidApplicationId = 32,
        NoContent = 64,
        InvalidVersion = 128
    }
}