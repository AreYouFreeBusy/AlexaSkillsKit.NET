// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Speechlet
{
    [Obsolete("Does not support context object. Derive from SpeechletBase instead and implement ISpeechletWithContextAsync")]
    public abstract class SpeechletAsync : SpeechletBase, ISpeechletAsync
    {
        public abstract Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
        public abstract Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        public abstract Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
        public abstract Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
    }
}