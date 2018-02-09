using AlexaSkillsKit.Interfaces.Dialog.Directives;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.FunctionalTests
{
    [TestClass]
    public class SampleIntent3HandlerTests : TestsBase
    {
        private Slot slot1;
        private Slot slot2;
        private Slot slot3;

        public SampleIntent3HandlerTests() {
            intent.Name = IntentNames.SampleIntent3;
            slot1 = intent.Slots[SlotNames.SampleSlot1] = new Slot { Name = SlotNames.SampleSlot1 };
            slot2 = intent.Slots[SlotNames.SampleSlot2] = new Slot { Name = SlotNames.SampleSlot2 };
            slot3 = intent.Slots[SlotNames.SampleSlot3] = new Slot { Name = SlotNames.SampleSlot3 };
        }

        [TestMethod]
        public async Task SampleIntent3HandlerSlot1HasValue() {
            slot1.Value = "SampleValue1";
            slot2.Value = "SampleValue2";
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsFalse(keepSessionCalled);
            Assert.IsTrue(outputText.Contains("Slot1"));
            Assert.IsTrue(outputText.Contains(slot1.Value));
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent3HandlerSlot2HasValue() {
            slot2.Value = "SampleValue2";
            slot3.Value = "SampleValue3";
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsFalse(keepSessionCalled);
            Assert.IsTrue(outputText.Contains("Slot2"));
            Assert.IsTrue(outputText.Contains(slot2.Value));
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent3HandlerSlot3HasValueStarted() {
            slot3.Value = "SampleValue3";
            slot3.ConfirmationStatus = ConfirmationStatusEnum.NONE;
            var request = BuildRequest(intent, true, IntentRequest.DialogStateEnum.STARTED);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsTrue(outputText.Contains("Slot3"));
            Assert.IsTrue(outputText.Contains(slot3.Value));
            Assert.AreEqual(1, outputDirectives.Count);
            Assert.IsInstanceOfType(outputDirectives.First(), typeof(DialogConfirmSlotDirective));
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent3HandlerSlot3HasValueInProgress() {
            slot3.Value = "SampleValue3";
            slot3.ConfirmationStatus = ConfirmationStatusEnum.NONE;
            var request = BuildRequest(intent, true, IntentRequest.DialogStateEnum.IN_PROGRESS);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsNull(outputText);
            Assert.AreEqual(1, outputDirectives.Count);
            Assert.IsInstanceOfType(outputDirectives.First(), typeof(DialogDelegateDirective));
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent3HandlerSlot3HasValueConfirmed() {
            slot3.Value = "SampleValue3";
            slot3.ConfirmationStatus = ConfirmationStatusEnum.CONFIRMED;
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsFalse(keepSessionCalled);
            Assert.IsTrue(outputText.Contains("Slot3"));
            Assert.IsTrue(outputText.Contains(slot3.Value));
            Assert.IsTrue(outputText.Contains("confirmed"));
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent3HandlerSlot3HasValueRejected() {
            slot3.Value = "SampleValue3";
            slot3.ConfirmationStatus = ConfirmationStatusEnum.DENIED;
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsFalse(keepSessionCalled);
            Assert.IsTrue(outputText.Contains("Slot3"));
            Assert.IsTrue(outputText.Contains(slot3.Value));
            Assert.IsTrue(outputText.Contains("rejected"));
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent3HandlerNoValue() {
            var request = BuildRequest(intent, false);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsNotNull(outputText);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }
    }
}