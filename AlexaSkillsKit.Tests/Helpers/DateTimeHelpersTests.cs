// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using System.IO;
using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;
using Xunit;

namespace AlexaSkillsKit.Tests.Helpers
{
    public class DateTimeHelpersTests
    {
        [Fact]
        public void RequestWithIso8601TimestampTest() {
            const string TestDataFile = @"TestData\RequestWithIso8601Timestamp.json";
    
            SpeechletRequestValidationResult validationResult = SpeechletRequestValidationResult.OK;
            SpeechletRequestEnvelope alexaRequest = null;
            var alexaContent = File.ReadAllText(TestDataFile);

            try {
                alexaRequest = SpeechletRequestEnvelope.FromJson(alexaContent);
            }
            catch (Exception ex)
            when (ex is Newtonsoft.Json.JsonReaderException || ex is InvalidCastException || ex is FormatException) {
                validationResult = validationResult | SpeechletRequestValidationResult.InvalidJson;
            }

            Assert.True(validationResult == SpeechletRequestValidationResult.OK);
        }


        [Fact]
        public void RequestWithUnixTimeTimestampTest() {
            const string TestDataFile = @"TestData\RequestWithUnixTimeTimestamp.json";

            SpeechletRequestValidationResult validationResult = SpeechletRequestValidationResult.OK;
            SpeechletRequestEnvelope alexaRequest = null;
            var alexaContent = File.ReadAllText(TestDataFile);

            try {
                alexaRequest = SpeechletRequestEnvelope.FromJson(alexaContent);
            }
            catch (Exception ex)
            when (ex is Newtonsoft.Json.JsonReaderException || ex is InvalidCastException || ex is FormatException) {
                validationResult = validationResult | SpeechletRequestValidationResult.InvalidJson;
            }

            Assert.True(validationResult == SpeechletRequestValidationResult.OK);
        }


        [Fact]
        public void RequestWithInvalidTimestampTest(){
            const string TestDataFile = @"TestData\RequestWithInvalidTimestamp.json";

            SpeechletRequestValidationResult validationResult = SpeechletRequestValidationResult.OK;
            SpeechletRequestEnvelope alexaRequest = null;
            var alexaContent = File.ReadAllText(TestDataFile);

            try {
                alexaRequest = SpeechletRequestEnvelope.FromJson(alexaContent);
            }
            catch (Exception ex)
            when (ex is Newtonsoft.Json.JsonReaderException || ex is InvalidCastException || ex is FormatException) {
                validationResult = validationResult | SpeechletRequestValidationResult.InvalidJson;
            }

            Assert.True(validationResult == SpeechletRequestValidationResult.InvalidJson);
        }
    }
}
