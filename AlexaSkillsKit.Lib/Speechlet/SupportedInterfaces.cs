using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using AlexaSkillsKit.Speechlet.Interfaces.Display;
using AlexaSkillsKit.Speechlet.Interfaces.AudioPlayer;

namespace AlexaSkillsKit.Speechlet
{
    public class SupportedInterfaces : Dictionary<string, ISpeechletInterface>
    {
        /// <summary>
        /// Register supported interfaces for deserialization
        /// </summary>
        static SupportedInterfaces() {
            Deserializer<ISpeechletInterface>.RegisterDeserializer("Display", DisplayInterface.FromJson);
            Deserializer<ISpeechletInterface>.RegisterDeserializer("AudioPlayer", AudioPlayerInterface.FromJson);
        }

        private SupportedInterfaces(IDictionary<string, ISpeechletInterface> dictionary) : base(dictionary) { }

        public static SupportedInterfaces FromJson(JObject json)
        {
            if (json == null) return null;

            var dictionary = json?.Children<JProperty>()
                .ToDictionary(x => x.Name, x => Deserializer<ISpeechletInterface>.FromJson(x));

            return new SupportedInterfaces(dictionary);
        }
    }
}