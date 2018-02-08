using AlexaSkillsKit.Slu;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers
{
    public static class DialogHelper
    {
        public static bool? ToBool(this ConfirmationStatusEnum confirmationStatus) {
            switch (confirmationStatus) {
                case ConfirmationStatusEnum.CONFIRMED:
                    return true;
                case ConfirmationStatusEnum.DENIED:
                    return false;
            }
            return null;
        }
    }
}