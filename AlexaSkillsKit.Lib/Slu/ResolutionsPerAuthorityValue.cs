// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using Newtonsoft.Json.Linq;

namespace AlexaSkillsKit.Slu
{
    public class ResolutionsPerAuthorityValue
    {
        public static ResolutionsPerAuthorityValue FromJson(JObject json) {
            if (json == null) return null;

            return new ResolutionsPerAuthorityValue {
                Value = ResolutionsPerAuthorityValueValue.FromJson(json.Value<JObject>("value"))
            };
        }

        public virtual ResolutionsPerAuthorityValueValue Value {
            get;
            set;
        }
    }
}