//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public abstract class SpeechletAsync : SpeechletBase, ISpeechletAsync
    {
        public abstract Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context);
        public abstract Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        public abstract Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
        public abstract Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
    }
}