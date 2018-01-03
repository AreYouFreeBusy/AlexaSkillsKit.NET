using Newtonsoft.Json.Serialization;
using System;

namespace AlexaSkillsKit.Json
{
    public class CamelCasePropertyNamesExceptDictionaryKeysContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType) {
            var contract = base.CreateDictionaryContract(objectType);
            contract.DictionaryKeyResolver = propertyName => propertyName;
            return contract;
        }
    }
}