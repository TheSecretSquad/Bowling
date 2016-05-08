using Bowling;
using Bowling.Printing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class BowlingScoreCardTest
    {
        private BowlingScoreCard bowlingScoreCard;
        private Frames frames;
        private Frame frame;
        private Bonuses bonuses;
        private IBowlingScoreCardPrinter bowlingScoreCardPrinter;

        [TestInitialize]
        public void Setup()
        {
            frames = Mock.Of<Frames>();
            frame = Mock.Of<Frame>();
            bonuses = Mock.Of<Bonuses>();
            bowlingScoreCardPrinter = Mock.Of<IBowlingScoreCardPrinter>();
            bowlingScoreCard = new BowlingScoreCard(frames, bonuses);
        }

        [TestMethod]
        public void WhenAFrameIsWritten_RecordsTheFrame()
        {
            bowlingScoreCard.WriteFrame(frame);

            Mock.Get(frames).Verify(frs => frs.RecordFrame(frame));
        }

        [TestMethod]
        public void WhenAFrameIsWritten_UpdatesBonusesWithNewFrame()
        {
            bowlingScoreCard.WriteFrame(frame);

            Mock.Get(bonuses).Verify(bs => bs.UpdateWithFramesNewFrame(frames, frame));
        }

        [TestMethod]
        public void WhenPrinting_BeginsAndEndsPrinting()
        {
            bowlingScoreCard.PrintOn(bowlingScoreCardPrinter);

            Mock.Get(bowlingScoreCardPrinter).Verify(bscp => bscp.BeginPrint(bowlingScoreCard));
            Mock.Get(bowlingScoreCardPrinter).Verify(bscp => bscp.EndPrint(bowlingScoreCard));
        }

        [TestMethod]
        public void WhenPrinting_PrintsAllFrames()
        {
            bowlingScoreCard.PrintOn(bowlingScoreCardPrinter);

            Mock.Get(frames).Verify(frs => frs.PrintOn(bowlingScoreCardPrinter));
        }
    }
}
