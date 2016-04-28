namespace Bowling
{
    public interface IBonus
    {
        void RequestFromFrames(Frames frames);

        void ContributeThrowForFrame(Throw aThrow, Frame frame);
    }
}
