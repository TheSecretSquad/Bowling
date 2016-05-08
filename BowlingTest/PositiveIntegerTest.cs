﻿using Bowling;
using Bowling.Printing;
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
        public void ConstructsWithCorrectValueFromOtherPositiveInteger()
        {
            PositiveInteger ten = new PositiveInteger(10);
            Assert.AreEqual(ten, new PositiveInteger(ten));
        }

        [TestMethod]
        public void IncrementOperatorReturnsIncrementedValue()
        {
            PositiveInteger value = new PositiveInteger(5);
            Assert.AreEqual(new PositiveInteger(5), value++);
            Assert.AreEqual(new PositiveInteger(6), value);
            Assert.AreEqual(new PositiveInteger(7), ++value);
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

            Mock.Get(positiveIntegerPrinter).Verify(pip => pip.PrintPositiveIntValue(5));
        }
    }
}
