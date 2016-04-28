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
        public void BowlingAllStrikesBowls12Strikes()
        {
            sampleGame.BowlAllStrikes();
            Mock.Get(bowlingScore).Verify(bsc => bsc.WriteFrame(It.IsAny<Frame>()), Times.Exactly(12));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(It.IsAny<int>()), Times.Exactly(12));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(1));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(2));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(3));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(4));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(5));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(6));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(7));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(8));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(9));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(10));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(11));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(12));
        }
    }
}
