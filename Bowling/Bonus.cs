using System;

namespace Bowling
{
    public class Bonus : IBonus
    {
        public static IBonus Strike(int toFrameNumber)
        {
            return new Bonus(toFrameNumber, 2);
        }

        private int contributeToFrameNumber;
        private int takeFromFrameNumber;
        private int numberOfThrowsRequired;
        private int numberOfThrowsReceived;

        public Bonus(int toFrameNumber, int numberOfThrowsRequired)
        {
            if (toFrameNumber < 0)
                throw new NegativeFrameNumberException();

            this.contributeToFrameNumber = toFrameNumber;
            this.numberOfThrowsRequired = numberOfThrowsRequired;
            this.numberOfThrowsReceived = 0;
            this.takeFromFrameNumber = toFrameNumber + 1;
        }

        public void ContributeThrowForFrame(Throw aThrow, Frame frame)
        {
            IfBonusIsNotFulfilledDo(() => AcceptBonusThrowForFrame(aThrow, frame));
        }

        private void AcceptBonusThrowForFrame(Throw aThrow, Frame frame)
        {
            AdvanceBonus();
            AcceptThrowForFrame(aThrow, frame);
        }

        private void AdvanceBonus()
        {
            ReceivedThrow();
            TargetNextFrameNumber();
        }

        private void ReceivedThrow() => ++numberOfThrowsReceived;

        private void TargetNextFrameNumber() => ++takeFromFrameNumber;

        private void AcceptThrowForFrame(Throw aThrow, Frame frame) => frame.AcceptThrow(aThrow);

        private void IfBonusIsNotFulfilledDo(Action action)
        {
            if (!IsBonusFulfilled())
                action();
        }

        private bool IsBonusFulfilled() => numberOfThrowsReceived == numberOfThrowsRequired;

        public void RequestFromFrames(Frames frames)
        {
            IfBonusIsNotFulfilledDo(() => RequestBonusFromFrames(frames));
        }

        private void RequestBonusFromFrames(Frames frames) =>
            frames.RequestBonusFromFrameNumberToFrameNumber(takeFromFrameNumber, contributeToFrameNumber);
    }
}
