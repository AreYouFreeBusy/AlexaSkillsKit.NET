//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

namespace AlexaSkillsKit.Speechlet
{
    public abstract class Speechlet : SpeechletBase, ISpeechlet
    {
        protected Speechlet() {
            Service.Initialize(new SpeechletAsyncWrapper(this));
        }

        public abstract AudioPlayerResponse OnAudioPlayer(AudioPlayerRequest audioRequest, Context context);
        public abstract AudioPlayerResponse OnPlaybackController(PlaybackControllerRequest playbackRequest, Context context);
        public abstract SpeechletResponse OnDisplay(DisplayRequest displayRequest, Context context);
        public abstract void OnSystemExceptionEncountered(SystemExceptionEncounteredRequest systemRequest, Context context);

        public abstract SpeechletResponse OnIntent(IntentRequest intentRequest, Session session, Context context);
        public abstract SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session, Context context);
        public abstract void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
        public abstract void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
    }
}