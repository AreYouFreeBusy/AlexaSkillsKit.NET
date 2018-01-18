// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Speechlet;

namespace AlexaSkillsKit.Interfaces.Display
{
    public interface IDisplaySpeechlet
    {
        SpeechletResponse OnDisplay(DisplayRequest displayRequest, Context context);
    }
}