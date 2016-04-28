using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class PositiveIntegerTest
    {
        private IPositiveIntegerPrinter positiveIntegerPrinter;

        [TestInitialize]
        public void Setup()
        {
            positiveIntegerPrinter = Mock.Of<IPositiveIntegerPrinter>();
        }

        [TestMethod]
        public void DefaultValueIsZero()
        {
            Assert.AreEqual(new PositiveInteger(0), new PositiveInteger());
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeValueException))]
        public void DoesNotAcceptValuesLessThanZero()
        {
            new PositiveInteger(-1);
        }

        [TestMethod]
        public void SameValuesAreEqual()
        {
            Assert.AreEqual(new PositiveInteger(5), new PositiveInteger(5));
        }

        [TestMethod]
        public void DifferentValuesAreNotEqual()
        {
            Assert.AreNotEqual(new PositiveInteger(5), new PositiveInteger(4));
        }

        [TestMethod]
        public void NotEqualToNull()
        {
            Assert.AreNotEqual(new PositiveInteger(5), null);
        }

        [TestMethod]
        public void EqualsOperatorForDifferentInstancesWithSameValueIsTrue()
        {
            Assert.IsTrue(new PositiveInteger(5) == new PositiveInteger(5));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithSameValueIsFalse()
        {
            Assert.IsFalse(new PositiveInteger(5) != new PositiveInteger(5));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithDifferentValueIsTrue()
        {
            Assert.IsTrue(new PositiveInteger(5) != new PositiveInteger(4));
        }

        [TestMethod]
        public void AsIntegerCreatesIntegerWithSameValue()
        {
            Assert.AreEqual(5, new PositiveInteger(5).AsInteger());
        }

        [TestMethod]
        public void PrintingBeginsAndEndsPrinting()
        {
            PositiveInteger positiveInteger = new PositiveInteger();

            positiveInteger.PrintOn(positiveIntegerPrinter);

            Mock.Get(positiveIntegerPrinter).Verify(pip => pip.BeginPrint(positiveInteger));
            Mock.Get(positiveIntegerPrinter).Verify(pip => pip.EndPrint(positiveInteger));
        }

        [TestMethod]
        public void PrintingPrintsTheValue()
        {
            PositiveInteger positiveInteger = new PositiveInteger(5);

            positiveInteger.PrintOn(positiveIntegerPrinter);

            Mock.Get(positiveIntegerPrinter).Verify(pip => pip.PrintValue(5));
        }
    }
}
