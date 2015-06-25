//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public interface ISpeechletAsync
    {
        Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
        Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
        Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
    }
}