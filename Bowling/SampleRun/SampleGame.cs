using System;

namespace Bowling
{
    public class SampleGame
    {
        private IBowlingScoreCard bowlingScoreCard;
        private SampleGameFrameFactory sampleGameFrameFactory;

        public SampleGame(IBowlingScoreCard bowlingScoreCard, SampleGameFrameFactory sampleGameFrameFactory)
        {
            this.bowlingScoreCard = bowlingScoreCard;
            this.sampleGameFrameFactory = sampleGameFrameFactory;
        }

        public void BowlAllOpenFrames()
        {
            Bowl10FramesWithFrame((frameNumber) => sampleGameFrameFactory.Open());
        }

        public void BowlAllStrikes()
        {
            Bowl9FramesWithFrame(
                nextFrame: (frameNumber) => sampleGameFrameFactory.Strike(new FrameNumber(frameNumber)),
                tenthFrame: () => sampleGameFrameFactory.TenthFrameStrike(throw2: new Throw(10), throw3: new Throw(10)));
        }

        private void Bowl10FramesWithFrame(Func<int, Frame> nextFrame)
        {
            for (int frameNumber = 1; frameNumber <= 10; frameNumber++)
                bowlingScoreCard.WriteFrame(nextFrame(frameNumber));
        }

        private void Bowl9FramesWithFrame(Func<int, Frame> nextFrame, Func<Frame> tenthFrame)
        {
            for (int frameNumber = 1; frameNumber <= 9; frameNumber++)
                bowlingScoreCard.WriteFrame(nextFrame(frameNumber));

            bowlingScoreCard.WriteFrame(tenthFrame());
        }

        public void BowlAllSparesWithThrow1AndTenthFrameBonusThrow(Throw throw1, Throw tenthFrameThrow3)
        {
            Bowl9FramesWithFrame(
                nextFrame: (frameNumber) => sampleGameFrameFactory.Spare(new FrameNumber(frameNumber), throw1),
                tenthFrame: () => sampleGameFrameFactory.TenthFrameSpare(throw1, tenthFrameThrow3));
        }

        public void BowlAllMissedSpares(Throw throw1)
        {
            if (!throw1.HasSpare())
                throw new Exception("Expecting a throw with a spare");

            Bowl10FramesWithFrame((frameNumber) => new Frame(throw1, new Throw(0), new NoBonus(), new RunningTotal(0)));
        }
    }
}
