// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Interfaces.Display
{
    public interface IDisplaySpeechletAsync
    {
        Task<SpeechletResponse> OnDisplayAsync(DisplayRequest displayRequest, Context context);
    }
}