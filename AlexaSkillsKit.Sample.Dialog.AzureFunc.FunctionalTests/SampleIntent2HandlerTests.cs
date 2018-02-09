using AlexaSkillsKit.Sample.Dialog.AzureFunc.Helpers;
using AlexaSkillsKit.Slu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Sample.Dialog.AzureFunc.FunctionalTests
{
    [TestClass]
    public class SampleIntent2HandlerTests : TestsBase
    {
        private Slot slot;

        public SampleIntent2HandlerTests() {
            intent.Name = IntentNames.SampleIntent2;
            slot = intent.Slots[SlotNames.SampleSlot1] = new Slot { Name = SlotNames.SampleSlot1 };
        }

        [TestMethod]
        public async Task SampleIntent2HandlerNoValueNew() {
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsNotNull(outputText);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent2HandlerNoValueNotNew() {
            var request = BuildRequest(intent, false);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsNotNull(outputText);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent2HandlerHasValueNew() {
            slot.Value = "SampleValue1";
            var request = BuildRequest(intent);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsTrue(keepSessionCalled);
            Assert.IsNotNull(outputText);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }

        [TestMethod]
        public async Task SampleIntent2HandlerHasValueNotNew() {
            slot.Value = "SampleValue1";
            var request = BuildRequest(intent, false);

            var response = await target.ProcessRequestAsync(request);

            Assert.IsFalse(keepSessionCalled);
            Assert.IsNotNull(outputText);
            Assert.AreEqual(0, outputDirectives.Count);
            Mock.Get(responseBuilder).Verify(x => x.Build(), Times.Once);
        }
    }
}