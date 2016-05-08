using System;

namespace Bowling
{
    public class Bonus : IBonus
    {
        public static Bonus Strike(FrameNumber toFrameNumber)
        {
            return new Bonus(toFrameNumber, 2);
        }

        public static Bonus Spare(FrameNumber toFrameNumber)
        {
            return new Bonus(toFrameNumber, 1);
        }

        private FrameNumber contributeToFrameNumber;
        private FrameNumber takeFromFrameNumber;
        private PositiveInteger numberOfThrowsRequired;
        private PositiveInteger numberOfThrowsReceived;

        public Bonus(FrameNumber toFrameNumber, PositiveInteger numberOfThrowsRequired)
        {
            this.contributeToFrameNumber = toFrameNumber;
            this.numberOfThrowsRequired = numberOfThrowsRequired;
            this.numberOfThrowsReceived = new PositiveInteger(0);
            this.takeFromFrameNumber = toFrameNumber.AsInteger() + 1;
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

        private void ReceivedThrow() => numberOfThrowsReceived = numberOfThrowsReceived.AsInteger() + 1;

        private void TargetNextFrameNumber() => takeFromFrameNumber = NextFrameNumber(takeFromFrameNumber);

        private FrameNumber NextFrameNumber(FrameNumber frameNumber)
        {
            if (frameNumber == new FrameNumber(10))
                return frameNumber;

            return takeFromFrameNumber.AsInteger() + 1;
        }

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
