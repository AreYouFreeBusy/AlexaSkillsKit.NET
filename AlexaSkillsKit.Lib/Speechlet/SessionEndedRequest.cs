// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;
using System;

namespace AlexaSkillsKit.Speechlet
{
    public class SessionEndedRequest : SpeechletRequest
    {
        public SessionEndedRequest(JObject json) : base(json) {

            ReasonEnum reason = ReasonEnum.UNKNOWN;
            Enum.TryParse(json.Value<string>("reason"), out reason);
            Reason = reason;
            Error = Error.FromJson(json.Value<JObject>("error"));
        }

        public virtual ReasonEnum Reason {
            get;
            private set;
        }

        public Error Error {
            get;
            private set;
        }

        public enum ReasonEnum
        {
            NONE = 0, // default in case parsing fails (backwards compatibility)
            UNKNOWN = 0, // default in case parsing fails
            ERROR,
            USER_INITIATED,
            EXCEEDED_MAX_REPROMPTS,
        }
    }
}