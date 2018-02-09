using AlexaSkillsKit.Interfaces.Dialog.Directives;
using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Slu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.FunctionalTests
{
    [TestClass]
    public class SampleIntent1HandlerTests: TestsBase
    {
        private Slot slot;

        public SampleIntent1HandlerTests() {
            intent.Name = IntentNames.SampleIntent1;
            slot = intent.Slots[SlotNames.SampleSlot1] = new Slot { Name = SlotNames.SampleSlot1 };
        }

        [TestMethod]
        public async Task SampleIntent1HandlerNoValueNew() {
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsNotNull(outputText);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent1HandlerNoValueUnconfirmed() {
            intent.ConfirmationStatus = ConfirmationStatusEnum.NONE;
            var request = BuildRequest(intent, false);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsNotNull(outputText);
            Assert.AreEqual(1, outputDirectives.Count);
            Assert.IsInstanceOfType(outputDirectives.First(), typeof(DialogConfirmIntentDirective));
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent1HandlerHasValueUnconfirmed() {
            slot.Value = "SampleValue1";
            intent.ConfirmationStatus = ConfirmationStatusEnum.NONE;
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsNull(outputText);
            Assert.AreEqual(1, outputDirectives.Count);
            Assert.IsInstanceOfType(outputDirectives.First(), typeof(DialogDelegateDirective));
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent1HandlerRejected() {
            intent.ConfirmationStatus = ConfirmationStatusEnum.DENIED;
            var request = BuildRequest(intent, false);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsFalse(keepSessionCalled);
            Assert.IsNotNull(outputText);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent1HandlerHasValueConfirmed() {
            slot.Value = "SampleValue1";
            intent.ConfirmationStatus = ConfirmationStatusEnum.CONFIRMED;
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsFalse(keepSessionCalled);
            Assert.IsTrue(outputText.Contains(slot.Value));
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }
    }
}