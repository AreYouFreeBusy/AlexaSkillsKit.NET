using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers
{
    public interface IIntentHandler
    {
        Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
    }
}