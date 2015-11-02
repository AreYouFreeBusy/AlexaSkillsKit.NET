//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;

namespace AlexaSkillsKit.Speechlet
{
    public interface ISpeechlet
    {
        bool OnRequestValidation(
            SpeechletRequestValidationResult result, DateTime referenceTimeUtc, SpeechletRequestEnvelope requestEnvelope);
        
        SpeechletResponse OnIntent(IntentRequest intentRequest, Session session);
        SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
        void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
        void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
    }
}