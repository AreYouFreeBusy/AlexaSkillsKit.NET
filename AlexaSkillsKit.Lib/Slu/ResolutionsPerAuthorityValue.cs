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