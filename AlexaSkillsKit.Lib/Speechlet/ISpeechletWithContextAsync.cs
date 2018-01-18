// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    public interface ISpeechletWithContextAsync
    {
        Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context);
        Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session, Context context);
        Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session, Context context);
        Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session, Context context);
    }
}