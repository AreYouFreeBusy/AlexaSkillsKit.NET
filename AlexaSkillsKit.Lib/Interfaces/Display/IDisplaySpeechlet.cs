using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.Display
{
    public interface IDisplaySpeechlet
    {
        SpeechletResponse OnDisplay(DisplayRequest displayRequest, Context context);
    }
}