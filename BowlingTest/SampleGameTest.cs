using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest.TestHelperTest
{
    [TestClass]
    public class SampleGameTest
    {
        private SampleGame sampleGame;
        private SampleGameFrameFactory sampleGameFrameFactory;
        private IBowlingScoreCard bowlingScore;

        [TestInitialize]
        public void Setup()
        {
            bowlingScore = Mock.Of<IBowlingScoreCard>();
            sampleGameFrameFactory = Mock.Of<SampleGameFrameFactory>();
            sampleGame = new SampleGame(bowlingScore, sampleGameFrameFactory);
        }

        [TestMethod]
        public void BowlingAllOpenFramesBowls10OpenFrames()
        {
            sampleGame.BowlAllOpenFrames();
            Mock.Get(bowlingScore).Verify(bsc => bsc.WriteFrame(It.IsAny<Frame>()), Times.Exactly(10));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open(), Times.Exactly(10));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Open());
        }

        [TestMethod]
        public void BowlingAllStrikesBowls10Strikes()
        {
            sampleGame.BowlAllStrikes();
            Mock.Get(bowlingScore).Verify(bsc => bsc.WriteFrame(It.IsAny<Frame>()), Times.Exactly(10));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(1)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(3)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(4)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(5)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(6)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(7)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(8)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new FrameNumber(9)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.TenthFrameStrike(new Throw(10), new Throw(10)));
        }

        [TestMethod]
        public void BowlingAllSparesBowls10Spares()
        {
            sampleGame.BowlAllSparesWithThrow1AndTenthFrameBonusThrow(new Throw(2), new Throw(5));
            Mock.Get(bowlingScore).Verify(bsc => bsc.WriteFrame(It.IsAny<Frame>()), Times.Exactly(10));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(1), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(2), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(3), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(4), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(5), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(6), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(7), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(8), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Spare(new FrameNumber(9), new Throw(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.TenthFrameSpare(new Throw(2), new Throw(5)));
        }
    }
}
