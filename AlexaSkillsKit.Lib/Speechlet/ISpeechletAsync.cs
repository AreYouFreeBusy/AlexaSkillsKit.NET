//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Threading.Tasks;
using AlexaSkillsKit.Authentication;
using AlexaSkillsKit.Json;

namespace AlexaSkillsKit.Speechlet
{
    public interface ISpeechletAsync
    {
        bool OnRequestValidation(
            SpeechletRequestValidationResult result, DateTime referenceTimeUtc, SpeechletRequestEnvelope requestEnvelope);

        Task<AudioPlayerResponse> OnAudioPlayerAsync(AudioPlayerRequest audioRequest, Context context);
        Task<AudioPlayerResponse> OnPlaybackControllerAsync(PlaybackControllerRequest playbackRequest, Context context);
        Task OnSystemExceptionEncounteredAsync(SystemExceptionEncounteredRequest systemRequest, Context context);

        Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context);
        Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
        Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
    }
}