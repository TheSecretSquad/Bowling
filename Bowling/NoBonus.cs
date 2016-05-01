namespace Bowling
{
    public class NoBonus : IBonus
    {
        public void ContributeThrowForFrame(Throw aThrow, Frame frame) { /* Do nothing */ }

        public void RequestFromFrames(Frames frames) { /* Do nothing */ }
    }
}
