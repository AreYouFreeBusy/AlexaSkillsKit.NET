using AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Interfaces.AudioPlayer;
using AlexaSkillsKit.Speechlet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc
{
    public class SampleSkill : SpeechletBase, ISpeechletWithContextAsync, IAudioPlayerSpeechletAsync
    {
        private Dictionary<string, IIntentHandler> handlers = new Dictionary<string, IIntentHandler>();
        private IIntentHandler defaultHandler;
        public readonly ILogHelper logHelper;

        public SampleSkill(ILogHelper logHelper)
        {
            Service.ApplicationId = Environment.GetEnvironmentVariable("ApplicationId");
            this.logHelper = logHelper;
        }

        public void Register(string intent, IIntentHandler handler)
        {
            handlers[intent] = handler;
        }

        public void RegisterDefault(IIntentHandler handler)
        {
            defaultHandler = handler;
        }

        public async Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context) {
            await logHelper.Log($"{session.User.Id} in session {session.SessionId} asked for {intentRequest.Intent.Name}");
            IIntentHandler handler = handlers.TryGetValue(intentRequest.Intent.Name, out handler) ? handler : defaultHandler;
            return await handler.OnIntentAsync(intentRequest, session);
        }

        public async Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session, Context context)
        {
            await logHelper.Log($"User {session.User.Id} in session {session.SessionId} launched skill");
            return await defaultHandler.OnIntentAsync(null, session);
        }

        public async Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session, Context context)
        {
            await logHelper.Log($"User {session.User.Id} started session {session.SessionId}");
        }

        public async Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session, Context context)
        {
            switch (sessionEndedRequest.Reason) {
                case SessionEndedRequest.ReasonEnum.ERROR:
                    await logHelper.Log($"User {session.User.Id} ended session {session.SessionId} with error");
                    break;
                case SessionEndedRequest.ReasonEnum.EXCEEDED_MAX_REPROMPTS:
                    await logHelper.Log($"User {session.User.Id} ended session {session.SessionId} due to lack of matching uttenancies");
                    break;
                default:
                    await logHelper.Log($"User {session.User.Id} ended session {session.SessionId}");
                    break;
            }
        }

        public async Task OnSystemExceptionEncounteredAsync(SystemExceptionEncounteredRequest systemRequest, Context context) {
            await logHelper.Log($"Exception '{systemRequest.Error?.Message}' encountered in request {systemRequest.Cause.RequestId}");
        }

        public Task<AudioPlayerResponse> OnAudioPlayerAsync(AudioPlayerRequest audioRequest, Context context) {
            throw new NotImplementedException();
        }

        public Task<AudioPlayerResponse> OnPlaybackControllerAsync(PlaybackControllerRequest playbackRequest, Context context) {
            throw new NotImplementedException();
        }
    }
}