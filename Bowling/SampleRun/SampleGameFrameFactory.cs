namespace Bowling
{
    public class SampleGameFrameFactory
    {
        public virtual Frame Open()
        {
            return Frame.Open();
        }

        public virtual Frame Strike(int frameNumber)
        {
            return Frame.Strike(frameNumber);
        }
    }
}
