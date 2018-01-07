using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Interfaces.Display
{
    public interface IDisplaySpeechletAsync
    {
        Task<SpeechletResponse> OnDisplayAsync(DisplayRequest displayRequest, Context context);
    }
}