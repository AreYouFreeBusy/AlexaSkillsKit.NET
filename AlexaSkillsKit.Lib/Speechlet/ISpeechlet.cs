// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;

namespace AlexaSkillsKit.Speechlet
{
    [Obsolete("Does not support context object. Implement ISpeechletWithContext instead")]
    public interface ISpeechlet : ISpeechletBase
    {
        SpeechletResponse OnIntent(IntentRequest intentRequest, Session session);
        SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
        void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
        void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
    }
}