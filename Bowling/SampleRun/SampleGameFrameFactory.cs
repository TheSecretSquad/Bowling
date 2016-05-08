namespace Bowling
{
    public class SampleGameFrameFactory
    {
        public virtual Frame Open()
        {
            return Frame.Open();
        }

        public virtual Frame Strike(FrameNumber frameNumber)
        {
            return Frame.Strike(frameNumber);
        }

        public virtual Frame TenthFrameStrike(Throw throw2, Throw throw3)
        {
            return Frame.TenthFrameStrike(throw2, throw3);
        }

        public virtual Frame Spare(FrameNumber frameNumber, Throw throw1)
        {
            return Frame.Spare(frameNumber, throw1);
        }

        public virtual Frame TenthFrameSpare(Throw throw1, Throw throw3)
        {
            return Frame.TenthFrameSpare(throw1, throw3);
        }
    }
}
