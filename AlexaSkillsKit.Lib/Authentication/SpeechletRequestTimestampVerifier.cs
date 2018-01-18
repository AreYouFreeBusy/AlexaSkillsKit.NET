// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using System.Diagnostics;
using AlexaSkillsKit.Json;

namespace AlexaSkillsKit.Authentication
{
    public class SpeechletRequestTimestampVerifier
    {
        /// <summary>
        /// Verifies request timestamp
        /// </summary>
        public static bool VerifyRequestTimestamp(SpeechletRequestEnvelope requestEnvelope, DateTime referenceTimeUtc) {
            // verify timestamp is within tolerance
            var diff = referenceTimeUtc - requestEnvelope.Request.Timestamp;
            Debug.WriteLine("Request was timestamped {0:0.00} seconds ago.", diff.TotalSeconds);
            return (Math.Abs((decimal)diff.TotalSeconds) <= Sdk.TIMESTAMP_TOLERANCE_SEC);
        }
    }
}