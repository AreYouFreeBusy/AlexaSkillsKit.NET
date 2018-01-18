// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    public interface IAudioPlayerSpeechletAsync
    {
        Task<AudioPlayerResponse> OnAudioPlayerAsync(AudioPlayerRequest audioRequest, Context context);
        Task<AudioPlayerResponse> OnPlaybackControllerAsync(PlaybackControllerRequest playbackRequest, Context context);
        Task OnSystemExceptionEncounteredAsync(SystemExceptionEncounteredRequest systemRequest, Context context);
    }
}