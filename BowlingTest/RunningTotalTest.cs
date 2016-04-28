using Bowling;
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
        [ExpectedException(typeof(NegativeValueException))]
        public void WhenConstruted_DoesNotAcceptValuesLessThanZero()
        {
            new RunningTotal(-1);
        }

        [TestMethod]
        public void SameValuesAreEqual()
        {
            Assert.AreEqual(new RunningTotal(5), new RunningTotal(5));
        }

        [TestMethod]
        public void DifferentValuesAreNotEqual()
        {
            Assert.AreNotEqual(new RunningTotal(5), new RunningTotal(4));
        }

        [TestMethod]
        public void NotEqualToNull()
        {
            Assert.AreNotEqual(new RunningTotal(5), null);
        }

        [TestMethod]
        public void EqualsOperatorForDifferentInstancesWithSameValueIsTrue()
        {
            Assert.IsTrue(new RunningTotal(5) == new RunningTotal(5));
        }

        [TestMethod]
        public void EqualsOperatorForDifferentInstancesWithDifferentValueIsFalse()
        {
            Assert.IsFalse(new RunningTotal(5) == new RunningTotal(4));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithSameValueIsFalse()
        {
            Assert.IsFalse(new RunningTotal(5) != new RunningTotal(5));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithDifferentValueIsTrue()
        {
            Assert.IsTrue(new RunningTotal(5) != new RunningTotal(4));
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
        public void GivenARunningTotalWithValue10_WhenRestartingFromNewTotal_GivesNewTotalWithSumOfNewRunningTotalThrows()
        {
            RunningTotal start = new RunningTotal(10);

            RunningTotal restarted = start.RestartFromTotalWithThrows(new RunningTotal(5), new Throw(1), new Throw(2));

            Assert.AreEqual(new RunningTotal(8), restarted);
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

            Mock.Get(runningTotalPrinter).Verify(rtp => rtp.PrintValue(0));
        }
    }
}
