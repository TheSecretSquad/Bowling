using Bowling;
using Bowling.Printers;
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
        public void TestImplicitConversionFromPositiveInteger()
        {
            FrameNumber one = new PositiveInteger(1);
        }

        [TestMethod]
        public void TestImplicitConversionFromInteger()
        {
            FrameNumber one = 1;
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
            PositiveInteger positiveInteger = Mock.Of<PositiveInteger>();
            Mock.Get(positiveInteger).Setup(e => e.AsInteger()).Returns(1);
            FrameNumber frameNumber = new FrameNumber(positiveInteger);

            frameNumber.PrintOn(frameNumberPrinter);

            Mock.Get(positiveInteger).Verify(pi => pi.PrintOn(frameNumberPrinter));
        }
    }
}
