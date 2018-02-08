using AlexaSkillsKit.Interfaces.Dialog.Directives;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Handlers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Builder;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers.Log;
using AlexaSkillsKit.Speechlet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.FunctionalTests
{
    [TestClass]
    public class SampleIntent1HandlerTests
    {
        private ILogHelper logHelper = Mock.Of<ILogHelper>();
        private ISpeechletResponseBuilder responseBuilder = Mock.Of<ISpeechletResponseBuilder>();
        private SpeechletResponse speechletResponse = new SpeechletResponse();

        private SpeechletBase target;

        private string outputText = null;
        private List<Directive> outputDirectives = new List<Directive>();
        private bool keepSessionCalled = false;

        public SampleIntent1HandlerTests() {
            Environment.SetEnvironmentVariable("ApplicationId", "amzn1.ask.skill.NNN");
            Mock.Get(responseBuilder).Setup(x => x.Say(It.IsAny<string>())).Returns(responseBuilder).Callback<string>(value => outputText = value);
            Mock.Get(responseBuilder).Setup(x => x.KeepSession()).Returns(responseBuilder).Callback(() => keepSessionCalled = true);
            Mock.Get(responseBuilder).Setup(x => x.WithDirective(It.IsAny<Directive>())).Returns(responseBuilder).Callback<Directive>(value => outputDirectives.Add(value));
            Mock.Get(responseBuilder).Setup(x => x.Build()).Returns(speechletResponse);

            target = InitializeApp();
        }

        [TestMethod]
        public async Task SampleIntent1HandlerHasValueUnconfirmed() {
            var request = @"{""session"": {""new"": true,""sessionId"": ""SessionId.111111"",
""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""attributes"": { },""user"": {""userId"": ""amzn1.ask.account.XXXX""}},
""request"": {""type"": ""IntentRequest"",""requestId"": ""EdwRequestId.000000000"",
""intent"": {""name"": ""SampleIntent1"",
""slots"": {""SampleSlot1"": {""name"": ""SampleSlot1"",""value"": ""Ivan""}}},
""locale"": ""en-US"",""timestamp"": ""2017-12-04T13:05:25Z"",
""dialogState"": ""STARTED""},
""context"": {""AudioPlayer"": {""playerActivity"": ""IDLE""},
""System"": {""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""user"": {""userId"": ""amzn1.ask.account.XXXX""},""device"": {""supportedInterfaces"": { }}}},""version"": ""1.0""}";

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.AreEqual(1, outputDirectives.Count);
            Assert.IsInstanceOfType(outputDirectives.First(), typeof(DialogDelegateDirective));
            Mock.Get(responseBuilder).Verify(x => x.Say(It.IsAny<string>()), Times.Never);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent1HandlerHasValueRejected() {
            var request = @"{""session"": {""new"": false,""sessionId"": ""SessionId.111111"",
""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""attributes"": { },""user"": {""userId"": ""amzn1.ask.account.XXXX""}},
""request"": {""type"": ""IntentRequest"",""requestId"": ""EdwRequestId.000000000"",
""intent"": {""name"": ""SampleIntent1"",
""slots"": {""SampleSlot1"": {""name"": ""SampleSlot1"",""value"": ""Ivan""}}},
""locale"": ""en-US"",""timestamp"": ""2017-12-04T13:05:25Z"",
""dialogState"": ""STARTED""},
""context"": {""AudioPlayer"": {""playerActivity"": ""IDLE""},
""System"": {""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""user"": {""userId"": ""amzn1.ask.account.XXXX""},""device"": {""supportedInterfaces"": { }}}},""version"": ""1.0""}";

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.AreEqual(1, outputDirectives.Count);
            Assert.IsInstanceOfType(outputDirectives.First(), typeof(Dia));
            Mock.Get(responseBuilder).Verify(x => x.Say(It.IsAny<string>()), Times.Never);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent1HandlerStarted() {
            var request = @"{""session"": {""new"": true,""sessionId"": ""SessionId.111111"",
""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""attributes"": { },""user"": {""userId"": ""amzn1.ask.account.XXXX""}},
""request"": {""type"": ""IntentRequest"",""requestId"": ""EdwRequestId.000000000"",
""intent"": {""name"": ""SampleIntent1"",
""slots"": {""SampleSlot1"": {""name"": ""SampleSlot1""}}},
""locale"": ""en-US"",""timestamp"": ""2017-12-04T13:05:25Z"",
""dialogState"": ""STARTED""},
""context"": {""AudioPlayer"": {""playerActivity"": ""IDLE""},
""System"": {""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""user"": {""userId"": ""amzn1.ask.account.XXXX""},""device"": {""supportedInterfaces"": { }}}},""version"": ""1.0""}";

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Say(It.IsAny<string>()), Times.Once);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent1HandlerInProgress() {
            var request = @"{""session"": {""new"": true,""sessionId"": ""SessionId.111111"",
""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""attributes"": { },""user"": {""userId"": ""amzn1.ask.account.XXXX""}},
""request"": {""type"": ""IntentRequest"",""requestId"": ""EdwRequestId.000000000"",
""intent"": {""name"": ""SampleIntent1"",
""slots"": {""SampleSlot1"": {""name"": ""SampleSlot1""}}},
""locale"": ""en-US"",""timestamp"": ""2017-12-04T13:05:25Z"",
""dialogState"": ""IN_PROGRESS""},
""context"": {""AudioPlayer"": {""playerActivity"": ""IDLE""},
""System"": {""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""user"": {""userId"": ""amzn1.ask.account.XXXX""},""device"": {""supportedInterfaces"": { }}}},""version"": ""1.0""}";

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Say(It.IsAny<string>()), Times.Once);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent1HandlerNoValue() {
            var request = @"{""session"": {""new"": true,""sessionId"": ""SessionId.111111"",
""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""attributes"": { },""user"": {""userId"": ""amzn1.ask.account.XXXX""}},
""request"": {""type"": ""IntentRequest"",""requestId"": ""EdwRequestId.000000000"",
""intent"": {""name"": ""SampleIntent1"",
""slots"": {""SampleSlot1"": {""name"": ""SampleSlot1""}}},
""locale"": ""en-US"",""timestamp"": ""2017-12-04T13:05:25Z"",
""dialogState"": ""COMPLETED""},
""context"": {""AudioPlayer"": {""playerActivity"": ""IDLE""},
""System"": {""application"": {""applicationId"": ""amzn1.ask.skill.NNN""},
""user"": {""userId"": ""amzn1.ask.account.XXXX""},""device"": {""supportedInterfaces"": { }}}},""version"": ""1.0""}";

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Say(It.IsAny<string>()), Times.Once);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        private SpeechletBase InitializeApp() {
            var app = new SampleSkill(logHelper);
            app.Register(IntentNames.SampleIntent1, new SampleIntent1Handler(responseBuilder, logHelper));
            app.Register(IntentNames.SampleIntent2, new SampleIntent2Handler(responseBuilder, logHelper));
            app.Register(IntentNames.SampleIntent3, new SampleIntent3Handler(responseBuilder, logHelper));
            app.RegisterDefault(new DefaultHandler(responseBuilder, logHelper));
            return app;
        }
    }
}