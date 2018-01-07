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