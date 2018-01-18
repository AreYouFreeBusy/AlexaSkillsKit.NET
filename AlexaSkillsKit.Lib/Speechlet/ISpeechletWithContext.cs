// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

namespace AlexaSkillsKit.Speechlet
{
    public interface ISpeechletWithContext
    {
        SpeechletResponse OnIntent(IntentRequest intentRequest, Session session, Context context);
        SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session, Context context);
        void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session, Context context);
        void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session, Context context);
    }
}