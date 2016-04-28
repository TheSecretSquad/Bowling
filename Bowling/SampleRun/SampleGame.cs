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
            BowlNFramesWithFrame(10, (frameNumber) => sampleGameFrameFactory.Open());
        }

        public void BowlAllStrikes()
        {
            BowlNFramesWithFrame(12, (frameNumber) => sampleGameFrameFactory.Strike(frameNumber));
        }

        private void BowlNFramesWithFrame(int numberOfFrames, Func<int, Frame> nextFrame)
        {
            for (int frameNumber = 1; frameNumber <= numberOfFrames; frameNumber++)
                bowlingScoreCard.WriteFrame(nextFrame(frameNumber));
        }
    }
}
