//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

namespace AlexaSkillsKit.Speechlet
{
    public interface ISpeechlet : ISpeechletBase
    {
        SpeechletResponse OnIntent(IntentRequest intentRequest, Session session, Context context);
        SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
        void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
        void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
    }
}