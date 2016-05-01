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
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(It.IsAny<PositiveInteger>()), Times.Exactly(12));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(1)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(2)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(3)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(4)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(5)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(6)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(7)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(8)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(9)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(10)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(11)));
            Mock.Get(sampleGameFrameFactory).Verify(sgff => sgff.Strike(new PositiveInteger(12)));
        }
    }
}
