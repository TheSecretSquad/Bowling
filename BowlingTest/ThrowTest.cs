using Bowling;
using Bowling.Printing;
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
        [ExpectedException(typeof(BadThrowException))]
        public void DoesNotAcceptValuesHigherThanTen()
        {
            new Throw(11);
        }

        [TestMethod]
        public void AsIntegerGivesSameIntValue()
        {
            Assert.AreEqual(5, new Throw(5).AsInteger());
        }

        [TestMethod]
        public void AsRunningtotalGivesSameValueAsRunningTotal()
        {
            Assert.AreEqual(new RunningTotal(5), new Throw(5).AsRunningTotal());
        }

        [TestMethod]
        public void WhenPrinting_BeginsAndEndsPrinting()
        {
            Throw theThrow = new Throw();
            
            theThrow.PrintOn(throwPrinter);

            Mock.Get(throwPrinter).Verify(trp => trp.BeginPrint(theThrow));
            Mock.Get(throwPrinter).Verify(trp => trp.EndPrint(theThrow));
        }

        [TestMethod]
        public void WhenPrinting_PrintsThrowValue()
        {
            PositiveInteger positiveInteger = Mock.Of<PositiveInteger>();
            Throw theThrow = new Throw(positiveInteger);

            theThrow.PrintOn(throwPrinter);

            Mock.Get(positiveInteger).Verify(pi => pi.PrintOn(throwPrinter));
        }
    }
}
