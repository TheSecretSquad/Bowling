using System;

namespace Bowling
{
    public class Bonus : IBonus
    {
        public static IBonus Strike(PositiveInteger toFrameNumber)
        {
            return new Bonus(toFrameNumber, new PositiveInteger(2));
        }

        private PositiveInteger contributeToFrameNumber;
        private PositiveInteger takeFromFrameNumber;
        private PositiveInteger numberOfThrowsRequired;
        private PositiveInteger numberOfThrowsReceived;

        public Bonus(PositiveInteger toFrameNumber, PositiveInteger numberOfThrowsRequired)
        {
            this.contributeToFrameNumber = toFrameNumber;
            this.numberOfThrowsRequired = numberOfThrowsRequired;
            this.numberOfThrowsReceived = new PositiveInteger(0);
            this.takeFromFrameNumber = ++toFrameNumber;
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

        private void AcceptThrowForFrame(Throw aThrow, Frame frame) =>
            frame.AcceptThrow(aThrow);

        private void IfBonusIsNotFulfilledDo(Action action)
        {
            if (!IsBonusFulfilled())
                action();
        }

        private bool IsBonusFulfilled() =>
            numberOfThrowsReceived == numberOfThrowsRequired;

        public void RequestFromFrames(Frames frames)
        {
            IfBonusIsNotFulfilledDo(() => RequestBonusFromFrames(frames));
        }

        private void RequestBonusFromFrames(Frames frames) =>
            frames.RequestBonusFromFrameNumberToFrameNumber(takeFromFrameNumber, contributeToFrameNumber);
    }
}
