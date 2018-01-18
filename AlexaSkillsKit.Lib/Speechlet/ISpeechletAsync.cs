// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    [Obsolete("Does not support context object. Implement ISpeechletWithContextAsync instead")]
    public interface ISpeechletAsync : ISpeechletBase
    {
        Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
        Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
        Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
    }
}