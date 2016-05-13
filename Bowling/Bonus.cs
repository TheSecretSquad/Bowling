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
        private int numberOfThrowsRequired;
        private int numberOfThrowsReceived;

        public Bonus(FrameNumber toFrameNumber, int numberOfThrowsRequired)
        {
            this.contributeToFrameNumber = toFrameNumber;
            this.numberOfThrowsRequired = numberOfThrowsRequired;
            this.numberOfThrowsReceived = 0;
            this.takeFromFrameNumber = toFrameNumber.Next();
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

        private void TargetNextFrameNumber() => takeFromFrameNumber = takeFromFrameNumber.Next();

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
