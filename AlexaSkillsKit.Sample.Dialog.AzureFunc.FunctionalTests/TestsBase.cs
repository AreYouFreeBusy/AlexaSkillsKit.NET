using AlexaSkillsKit.Json;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.FunctionalTests
{
    public class TestsBase
    {
        private static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings() {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesExceptDictionaryKeysContractResolver(),
            Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() }
        };

        private const string RequestJson = @"{""session"": {""new"": <isNew>,""sessionId"": ""SessionId.111111"",
""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},""attributes"": { },""user"": {""userId"": ""amzn1.ask.account.XXXX""}},
""request"": {""type"": ""IntentRequest"",""requestId"": ""EdwRequestId.000000000"",""intent"": <intent>,
""locale"": ""en-US"",""timestamp"": ""2018-01-01T12:00:00Z"",""dialogState"": <dialogState>},
""context"": {""AudioPlayer"": {""playerActivity"": ""IDLE""},
""System"": {""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""user"": {""userId"": ""amzn1.ask.account.XXXX""},""device"": {""supportedInterfaces"": { }}}},""version"": ""1.0""}";

        protected readonly ILogHelper logHelper = Mock.Of<ILogHelper>();
        protected readonly ISpeechletResponseBuilder responseBuilder = Mock.Of<ISpeechletResponseBuilder>();
        protected readonly SpeechletResponse speechletResponse = new SpeechletResponse();

        protected readonly SpeechletBase target;
        protected readonly Intent intent;

        protected string outputText = null;
        protected List<Directive> outputDirectives = new List<Directive>();
        protected bool keepSessionCalled = false;

        protected TestsBase() {
            Environment.SetEnvironmentVariable("ApplicationId", "amzn1.ask.skill.NNN");

            Mock.Get(responseBuilder).Setup(x => x.Say(It.IsAny<string>())).Returns(responseBuilder).Callback<string>(value => outputText = value);
            Mock.Get(responseBuilder).Setup(x => x.KeepSession()).Returns(responseBuilder).Callback(() => keepSessionCalled = true);
            Mock.Get(responseBuilder).Setup(x => x.WithDirective(It.IsAny<Directive>())).Returns(responseBuilder).Callback<Directive>(value => outputDirectives.Add(value));
            Mock.Get(responseBuilder).Setup(x => x.Build()).Returns(speechletResponse);

            target = InitializeApp();
            intent = new Intent { Slots = new Dictionary<string, Slot>() };
    }

        private SpeechletBase InitializeApp() {
            var app = new SampleSkill(logHelper);
            app.Register(IntentNames.SampleIntent1, new SampleIntent1Handler(responseBuilder, logHelper));
            app.Register(IntentNames.SampleIntent2, new SampleIntent2Handler(responseBuilder, logHelper));
            app.Register(IntentNames.SampleIntent3, new SampleIntent3Handler(responseBuilder, logHelper));
            app.RegisterDefault(new DefaultHandler(responseBuilder, logHelper));
            return app;
        }

        protected string BuildRequest(Intent intent, bool isNewSession = true,
            IntentRequest.DialogStateEnum dialogState = IntentRequest.DialogStateEnum.STARTED) {
            return RequestJson
                .Replace("<isNew>", isNewSession.ToString().ToLower())
                .Replace("<intent>", JsonConvert.SerializeObject(intent, SerializerSettings))
                .Replace("<dialogState>", ((int)dialogState).ToString());
        }
    }
}