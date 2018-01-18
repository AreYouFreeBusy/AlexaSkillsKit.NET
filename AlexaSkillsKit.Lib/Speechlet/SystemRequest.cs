// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Speechlet
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html#system-exceptionencountered
    /// </summary>
    public class SystemRequest : ExtendedSpeechletRequest
    {
        public static readonly string TypeName = "System";

        public SystemRequest(string subtype, JObject json) : base(TypeName, subtype, json) {
        }
    }
}