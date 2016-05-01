namespace Bowling
{
    public class SampleGameFrameFactory
    {
        public virtual Frame Open()
        {
            return Frame.Open();
        }

        public virtual Frame Strike(PositiveInteger frameNumber)
        {
            return Frame.Strike(frameNumber);
        }
    }
}
