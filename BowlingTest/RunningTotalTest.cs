using Bowling;
using Bowling.Printing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class RunningTotalTest
    {
        private RunningTotal runningTotal;
        private IRunningTotalPrinter runningTotalPrinter;

        [TestInitialize]
        public void Setup()
        {
            runningTotal = new RunningTotal();
            runningTotalPrinter = Mock.Of<IRunningTotalPrinter>();
        }

        [TestMethod]
        public void DefaultValueIsZero()
        {
            Assert.AreEqual(new RunningTotal(0), new RunningTotal());
        }

        [TestMethod]
        public void WhenAddingANullThrow_ReturnsSameRunningTotalValue()
        {
            RunningTotal result = runningTotal.AddThrow(null);

            Assert.AreEqual(runningTotal, result);
        }

        [TestMethod]
        public void GivenRunningTotalWithValue0_WhenAddingAThrow_GivesNewRunningTotalWithSameValueAsThrow()
        {
            RunningTotal result = runningTotal.AddThrow(new Throw(5));

            Assert.AreEqual(new RunningTotal(5), result);
            Assert.AreNotSame(runningTotal, result);
        }

        [TestMethod]
        public void AsIntegerGivesSameIntValue()
        {
            Assert.AreEqual(5, new RunningTotal(5).AsInteger());
        }

        [TestMethod]
        public void WhenPrinting_BeginsAndEndsPrinting()
        {
            runningTotal.PrintOn(runningTotalPrinter);

            Mock.Get(runningTotalPrinter).Verify(rtp => rtp.BeginPrint(runningTotal));
            Mock.Get(runningTotalPrinter).Verify(rtp => rtp.EndPrint(runningTotal));
        }

        [TestMethod]
        public void WhenPrinting_PrintsTotal()
        {
            runningTotal.PrintOn(runningTotalPrinter);

            Mock.Get(runningTotalPrinter).Verify(rtp => rtp.PrintPositiveIntValue(0));
        }
    }
}
