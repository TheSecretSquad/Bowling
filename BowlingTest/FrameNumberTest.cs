using Bowling;
using Bowling.Printing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class FrameNumberTest
    {
        private IFrameNumberPrinter frameNumberPrinter;

        [TestInitialize]
        public void Setup()
        {
            frameNumberPrinter = Mock.Of<IFrameNumberPrinter>();
        }

        [TestMethod]
        public void DefaultValueIs1()
        {
            Assert.AreEqual(new FrameNumber(1), new FrameNumber());
        }

        [TestMethod]
        [ExpectedException(typeof(BadFrameNumberException))]
        public void DoesNotAcceptValuesHigherThanTen()
        {
            new FrameNumber(11);
        }

        [TestMethod]
        public void AsIntegerGivesSameIntValue()
        {
            Assert.AreEqual(5, new FrameNumber(5).AsInteger());
        }

        [TestMethod]
        public void GivenFrameNumber6_WhenNext_ReturnsFrameNumber7()
        {
            Assert.AreEqual(new FrameNumber(7), new FrameNumber(6).Next());
        }

        [TestMethod]
        public void GivenFrameNumber10_WhenNext_ReturnsFrameNumber10()
        {
            Assert.AreEqual(new FrameNumber(10), new FrameNumber(10).Next());
        }

        [TestMethod]
        public void WhenPrinting_BeginsAndEndsPrinting()
        {
            FrameNumber frameNumber = new FrameNumber();

            frameNumber.PrintOn(frameNumberPrinter);

            Mock.Get(frameNumberPrinter).Verify(fnp => fnp.BeginPrint(frameNumber));
            Mock.Get(frameNumberPrinter).Verify(fnp => fnp.EndPrint(frameNumber));
        }

        [TestMethod]
        public void WhenPrinting_PrintsFrameNumberValue()
        {
            FrameNumber frameNumber = new FrameNumber(5);

            frameNumber.PrintOn(frameNumberPrinter);

            Mock.Get(frameNumberPrinter).Verify(fnp => fnp.PrintFrameNumber(5));
        }
    }
}
