using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log
{
    public interface ILogHelper
    {
        Task Log(string message);
    }
}