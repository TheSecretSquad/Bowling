using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest.TestHelperTest
{
    [TestClass]
    public class TestScoreCardPrinterTest
    {
        private TestScoreCardPrinter testScoreCardPrinter;
        private Frame unusedSourceFrame;
        private Throw unusedSourceThrow;
        private RunningTotal unusedSourceRunningTotal;

        private void PrintFrameWithThrowValues(int throwOneValue, int throwTwoValue)
        {
            PrintFrameWithThrowValuesAndTotal(throwOneValue, throwTwoValue, 0);
        }

        private void PrintFrameWithThrowValuesAndTotal(int throwOneValue, int throwTwoValue, int total)
        {
            testScoreCardPrinter.BeginPrint(unusedSourceFrame);

            testScoreCardPrinter.BeginPrint(unusedSourceThrow);
            testScoreCardPrinter.PrintValue(throwOneValue);
            testScoreCardPrinter.EndPrint(unusedSourceThrow);

            testScoreCardPrinter.BeginPrint(unusedSourceThrow);
            testScoreCardPrinter.PrintValue(throwTwoValue);
            testScoreCardPrinter.EndPrint(unusedSourceThrow);

            testScoreCardPrinter.BeginPrint(unusedSourceRunningTotal);
            testScoreCardPrinter.PrintValue(total);
            testScoreCardPrinter.EndPrint(unusedSourceRunningTotal);

            testScoreCardPrinter.EndPrint(unusedSourceFrame);
        }

        private void PrintFrameWithOneThrowValue(int throwOneValue)
        {
            PrintFrameWithOneThrowValueAndTotal(throwOneValue, 0);
        }

        private void PrintFrameWithOneThrowValueAndTotal(int throwOneValue, int total)
        {
            testScoreCardPrinter.BeginPrint(unusedSourceFrame);

            testScoreCardPrinter.BeginPrint(unusedSourceThrow);
            testScoreCardPrinter.PrintValue(throwOneValue);
            testScoreCardPrinter.EndPrint(unusedSourceThrow);

            testScoreCardPrinter.BeginPrint(unusedSourceRunningTotal);
            testScoreCardPrinter.PrintValue(total);
            testScoreCardPrinter.EndPrint(unusedSourceRunningTotal);

            testScoreCardPrinter.EndPrint(unusedSourceFrame);
        }

        [TestInitialize]
        public void Setup()
        {
            testScoreCardPrinter = new TestScoreCardPrinter();
            unusedSourceFrame = Mock.Of<Frame>();
            unusedSourceThrow = Mock.Of<Throw>();
            unusedSourceRunningTotal = Mock.Of<RunningTotal>();
        }

        [TestMethod]
        public void PrintingFramesAndThrowsCorrectlyAssociatesThrowsWithFrames()
        {
            PrintFrameWithThrowValues(2, 4);
            PrintFrameWithThrowValues(6, 2);

            testScoreCardPrinter.VerifyHasNumberOfFrames(2);
            testScoreCardPrinter.VerifyFrameNumberThrowNumberHasValue(1, 1, 2);
            testScoreCardPrinter.VerifyFrameNumberThrowNumberHasValue(1, 2, 4);
            testScoreCardPrinter.VerifyFrameNumberThrowNumberHasValue(2, 1, 6);
            testScoreCardPrinter.VerifyFrameNumberThrowNumberHasValue(2, 2, 2);
        }

        [TestMethod]
        public void PrintingFramesAndThrowsHasCorrectNumberOfThrowsPerFrame()
        {
            PrintFrameWithThrowValues(0, 0);
            PrintFrameWithThrowValues(0, 0);

            testScoreCardPrinter.VerifyEachFrameHasNumberOfThrows(2);
        }

        [TestMethod]
        public void PrintingFramesWithOneThrowHasCorrectNumberOfThrowsPerFrame()
        {
            PrintFrameWithOneThrowValue(0);
            PrintFrameWithOneThrowValue(0);

            testScoreCardPrinter.VerifyEachFrameHasNumberOfThrows(1);
        }

        [TestMethod]
        public void PrintingTotalAssociatesTotalWithCurrentFrame()
        {
            PrintFrameWithThrowValuesAndTotal(2, 4, 6);
            PrintFrameWithThrowValuesAndTotal(6, 2, 14);

            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(1, 6);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(2, 14);
        }
    }
}
