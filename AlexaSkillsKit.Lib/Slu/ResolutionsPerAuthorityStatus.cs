// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Slu
{
    public class ResolutionsPerAuthorityStatus
    {
        public static ResolutionsPerAuthorityStatus FromJson(JObject json) {
            if (json == null) return null;

            return new ResolutionsPerAuthorityStatus {
                Code = json.Value<string>("code")
            };
        }

        public virtual string Code {
            get;
            set;
        }
    }
}