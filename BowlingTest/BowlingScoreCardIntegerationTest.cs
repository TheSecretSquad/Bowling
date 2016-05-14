using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingTest
{
    [TestClass]
    public class BowlingScoreCardIntegerationTest
    {
        private BowlingScoreCard bowlingScoreCard;
        private SampleGame sampleGame;
        private TestScoreCardPrinter testScoreCardPrinter;

        private TestScoreCardPrinter TestScoreCard()
        {
            return new TestScoreCardPrinter();
        }

        [TestInitialize]
        public void Setup()
        {
            bowlingScoreCard = new BowlingScoreCard(new Frames(), new Bonuses());
            sampleGame = new SampleGame(bowlingScoreCard, new SampleGameFrameFactory());
            testScoreCardPrinter = new TestScoreCardPrinter();
        }

        [TestMethod]
        public void BowlingAllOpenFramesHas10Frames()
        {
            sampleGame.BowlAllOpenFrames();

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyHasNumberOfFrames(10);
        }

        [TestMethod]
        public void BowlingAllOpenFramesHasCorrectScoreForEachFrame()
        {
            sampleGame.BowlAllOpenFrames();

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(1, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(2, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(3, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(4, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(5, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(6, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(7, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(8, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(9, 0);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(10,0);
        }

        [TestMethod]
        public void BowlingAllOpenFramesEachFrameHasTwoThrows()
        {
            sampleGame.BowlAllOpenFrames();

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyEachFrameHasNumberOfThrows(2);
        }

        [TestMethod]
        public void BowlingAllStrikesHas10Frames()
        {
            sampleGame.BowlAllStrikes();

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyHasNumberOfFrames(10);
        }

        [TestMethod]
        public void BowlingAllStrikesHasCorrectScoreForEachFrame()
        {
            sampleGame.BowlAllStrikes();

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(1, 30);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(2, 60);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(3, 90);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(4, 120);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(5, 150);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(6, 180);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(7, 210);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(8, 240);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(9, 270);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(10, 300);
        }

        [TestMethod]
        public void BowlingAllStrikesEachFrameHasOneThrow()
        {
            sampleGame.BowlAllStrikes();

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyEachFrameNThroughMHasNumberOfThrows(1, 9, 1);
            testScoreCardPrinter.VerifyFrameNumberHasNumberOfThrows(10, 3);
        }

        [TestMethod]
        public void BowlingAllSparesHas10Frames()
        {
            sampleGame.BowlAllSparesWithThrow1AndTenthFrameBonusThrow(new Throw(2), new Throw(5));

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyHasNumberOfFrames(10);
        }

        [TestMethod]
        public void BowlingAllSparesHasCorrectScoreForEachFrame()
        {
            sampleGame.BowlAllSparesWithThrow1AndTenthFrameBonusThrow(new Throw(2), new Throw(5));

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(1, 12);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(2, 24);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(3, 36);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(4, 48);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(5, 60);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(6, 72);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(7, 84);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(8, 96);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(9, 108);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(10, 123);
        }

        [TestMethod]
        public void BowlingAllSparesEachFrameHasTwoThrows()
        {
            sampleGame.BowlAllSparesWithThrow1AndTenthFrameBonusThrow(new Throw(2), new Throw(5));

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyEachFrameNThroughMHasNumberOfThrows(1, 9, 2);
            testScoreCardPrinter.VerifyFrameNumberHasNumberOfThrows(10, 3);
        }

        [TestMethod]
        public void BowlingAllMissedSparesHas10Frames()
        {
            sampleGame.BowlAllMissedSpares(new Throw(5));

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyHasNumberOfFrames(10);
        }

        [TestMethod]
        public void BowlingAllMissedSparesHasCorrectScoreForEachFrame()
        {
            sampleGame.BowlAllMissedSpares(new Throw(5));

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(1, 5);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(2, 10);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(3, 15);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(4, 20);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(5, 25);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(6, 30);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(7, 35);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(8, 40);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(9, 45);
            testScoreCardPrinter.VerifyFrameNumberHasScoreValue(10, 50);
        }

        [TestMethod]
        public void BowlingAllMissedSparesFramesEachFrameHasTwoThrows()
        {
            sampleGame.BowlAllMissedSpares(new Throw(5));

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyEachFrameHasNumberOfThrows(2);
        }
    }
}
