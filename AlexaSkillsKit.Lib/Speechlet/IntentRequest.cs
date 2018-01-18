// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Slu;
using Newtonsoft.Json.Linq;
using System;

namespace AlexaSkillsKit.Speechlet
{
    public class IntentRequest : SpeechletRequest
    {
        public IntentRequest(JObject json) : base(json) {
            Intent = Intent.FromJson(json.Value<JObject>("intent"));

            DialogStateEnum dialogState = DialogStateEnum.UNKNOWN;
            Enum.TryParse(json.Value<string>("dialogState"), out dialogState);
            DialogState = dialogState;
        }

        public virtual Intent Intent {
            get;
            private set;
        }

        public virtual DialogStateEnum DialogState {
            get;
            private set;
        }

        public enum DialogStateEnum
        {
            UNKNOWN = 0, // default in case parsing fails
            STARTED,
            IN_PROGRESS,
            COMPLETED
        }
    }
}