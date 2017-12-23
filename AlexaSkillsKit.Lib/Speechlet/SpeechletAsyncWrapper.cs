using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;
using System;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public sealed class SpeechletAsyncWrapper : ISpeechletAsync
    {
        private readonly ISpeechlet speechlet;

        public SpeechletAsyncWrapper(ISpeechlet speechlet) {
            this.speechlet = speechlet;
        }

        public bool OnRequestValidation(
            SpeechletRequestValidationResult result, DateTime referenceTimeUtc, SpeechletRequestEnvelope requestEnvelope) {

            return speechlet.OnRequestValidation(result, referenceTimeUtc, requestEnvelope);
        }

        public async Task<AudioPlayerResponse> OnAudioPlayerAsync(AudioPlayerRequest audioRequest, Context context) {
            return speechlet.OnAudioPlayer(audioRequest, context);
        }

        public async Task<AudioPlayerResponse> OnPlaybackControllerAsync(PlaybackControllerRequest playbackRequest, Context context) {
            return speechlet.OnPlaybackController(playbackRequest, context);
        }

        public async Task<SpeechletResponse> OnDisplayAsync(DisplayRequest displayRequest, Context context) {
            return speechlet.OnDisplay(displayRequest, context);
        }

        public async Task OnSystemExceptionEncounteredAsync(SystemExceptionEncounteredRequest systemRequest, Context context) {
            speechlet.OnSystemExceptionEncountered(systemRequest, context);
        }

        public async Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context) {
            return speechlet.OnIntent(intentRequest, session, context);
        }

        public async Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session) {
            return speechlet.OnLaunch(launchRequest, session);
        }

        public async Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session) {
            speechlet.OnSessionEnded(sessionEndedRequest, session);
        }

        public async Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session) {
            speechlet.OnSessionStarted(sessionStartedRequest, session);
        }
    }
}