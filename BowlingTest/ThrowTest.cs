using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class ThrowTest
    {
        private IThrowPrinter throwPrinter;

        [TestInitialize]
        public void Setup()
        {
            throwPrinter = Mock.Of<IThrowPrinter>();
        }

        [TestMethod]
        public void DefaultValueIsZero()
        {
            Assert.AreEqual(new Throw(0), new Throw());
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeValueException))]
        public void DoesNotAcceptValuesLessThanZero()
        {
            new Throw(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(BadThrowException))]
        public void DoesNotAcceptValuesHigherThanTen()
        {
            new Throw(11);
        }

        [TestMethod]
        public void SameValuesAreEqual()
        {
            Assert.AreEqual(new Throw(5), new Throw(5));
        }

        [TestMethod]
        public void DifferentValuesAreNotEqual()
        {
            Assert.AreNotEqual(new Throw(5), new Throw(4));
        }

        [TestMethod]
        public void NotEqualToNull()
        {
            Assert.AreNotEqual(new Throw(5), null);
        }

        [TestMethod]
        public void EqualsOperatorForDifferentInstancesWithSameValueIsTrue()
        {
            Assert.IsTrue(new Throw(5) == new Throw(5));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithSameValueIsFalse()
        {
            Assert.IsFalse(new Throw(5) != new Throw(5));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithDifferentValueIsTrue()
        {
            Assert.IsTrue(new Throw(5) != new Throw(4));
        }

        [TestMethod]
        public void AsRunningTotalCreatesRunningTotalWithSameThrowValue()
        {
            Assert.AreEqual(new RunningTotal(5), new Throw(5).AsRunningTotal());
        }

        [TestMethod]
        public void PrintingThrowBeginsAndEndsPrinting()
        {
            Throw theThrow = new Throw();
            
            theThrow.PrintOn(throwPrinter);

            Mock.Get(throwPrinter).Verify(trp => trp.BeginPrint(theThrow));
            Mock.Get(throwPrinter).Verify(trp => trp.EndPrint(theThrow));
        }

        [TestMethod]
        public void PrintingThrowPrintsTheThrowValue()
        {
            PositiveInteger positiveInteger = Mock.Of<PositiveInteger>();
            Throw theThrow = new Throw(positiveInteger);

            theThrow.PrintOn(throwPrinter);

            Mock.Get(positiveInteger).Verify(pi => pi.PrintOn(throwPrinter));
        }
    }
}
