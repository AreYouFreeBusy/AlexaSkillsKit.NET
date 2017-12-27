using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.AudioPlayer
{
    public interface IAudioPlayerSpeechlet
    {
        AudioPlayerResponse OnAudioPlayer(AudioPlayerRequest audioRequest, Context context);
        AudioPlayerResponse OnPlaybackController(PlaybackControllerRequest playbackRequest, Context context);
        void OnSystemExceptionEncountered(SystemExceptionEncounteredRequest systemRequest, Context context);
    }
}