//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

namespace AlexaSkillsKit.Speechlet
{
    public abstract class Speechlet : SpeechletBase, ISpeechlet
    {
        public abstract SpeechletResponse OnIntent(IntentRequest intentRequest, Session session, Context context);
        public abstract SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
        public abstract void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
        public abstract void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
    }
}