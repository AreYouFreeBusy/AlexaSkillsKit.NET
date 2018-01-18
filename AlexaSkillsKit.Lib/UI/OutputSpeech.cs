// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.UI
{
    public abstract class OutputSpeech
    {
        public abstract string Type {
            get;
        }
    }
}