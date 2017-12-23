//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public abstract class SpeechletAsync : SpeechletBase, ISpeechletAsync
    {
        protected SpeechletAsync() {
            Service.Initialize(this);
        }

        public abstract Task<AudioPlayerResponse> OnAudioPlayerAsync(AudioPlayerRequest audioRequest, Context context);
        public abstract Task<AudioPlayerResponse> OnPlaybackControllerAsync(PlaybackControllerRequest playbackRequest, Context context);
        public abstract Task<SpeechletResponse> OnDisplayAsync(DisplayRequest displayRequest, Context context);
        public abstract Task OnSystemExceptionEncounteredAsync(SystemExceptionEncounteredRequest systemRequest, Context context);

        public abstract Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context);
        public abstract Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        public abstract Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
        public abstract Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
    }
}