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
        public void BowlingAllStrikesHas12Frames()
        {
            sampleGame.BowlAllStrikes();

            bowlingScoreCard.PrintOn(testScoreCardPrinter);

            testScoreCardPrinter.VerifyHasNumberOfFrames(12);
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

            testScoreCardPrinter.VerifyEachFrameHasNumberOfThrows(1);
        }
    }
}
