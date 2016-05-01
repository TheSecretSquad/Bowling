using System;

namespace Bowling
{
    public class Frame
    {
        public static Frame Open()
        {
            return new Frame();
        }

        public static Frame Strike(PositiveInteger frameNumber)
        {
            return new Frame(new Throw(10), null, Bonus.Strike(frameNumber), new RunningTotal());
        }

        private Throw throw1;
        private Throw throw2;
        private RunningTotal runningTotal;
        private IBonus bonus;
        private Frame nextFrame;

        public Frame()
        {
            throw1 = new Throw(0);
            throw2 = new Throw(0);
            bonus = new NoBonus();
            runningTotal = new RunningTotal();
        }

        public Frame(Throw throw1, Throw throw2, IBonus bonus, RunningTotal runningTotal)
        {
            this.throw1 = throw1;
            this.throw2 = throw2;
            this.runningTotal = runningTotal;
            this.bonus = bonus;
            this.nextFrame = null;

            StartRunningTotal();
        }

        private void StartRunningTotal() =>
            runningTotal = runningTotal.AddThrow(throw1).AddThrow(throw2);

        public virtual void AnnounceToBonuses(Bonuses bonuses)
        {
            bonuses.RememberBonus(bonus);
        }

        public virtual void ContributeFromFrame(Frame frame)
        {
            frame.ContributeToFrame(this);
        }

        public virtual void ContributeToFrame(Frame frame)
        {
            ContributeThrowToFrame(throw1, frame);
            ContributeThrowToFrame(throw2, frame);
        }

        private void ContributeThrowToFrame(Throw aThrow, Frame frame) =>
            IfThrowHasValueDo(aThrow, () => frame.ContributeThrow(aThrow));

        private void IfThrowHasValueDo(Throw aThrow, Action throwAction)
        {
            if (aThrow != null)
                throwAction();
        }

        public virtual void ContributeThrow(Throw aThrow)
        {
            bonus.ContributeThrowForFrame(aThrow, this);
        }

        public virtual void AcceptThrow(Throw aThrow)
        {
            runningTotal = runningTotal.AddThrow(aThrow);
            RestartNextFrameTotal();
        }

        private void RestartNextFrameTotal()
        {
            if(nextFrame != null)
                nextFrame.RebaseWithRunningTotal(runningTotal);
        }

        public virtual void InitializeNextFrame(Frame frame)
        {
            nextFrame = frame;
            RestartNextFrameTotal();
        }

        public virtual void RebaseWithRunningTotal(RunningTotal runningTotal)
        {
            this.runningTotal = runningTotal.AddThrow(throw1).AddThrow(throw2);
        }

        public virtual void PrintOn(IFramePrinter framePrinter)
        {
            framePrinter.BeginPrint(this);
            PrintThrowsOnPrinter(framePrinter);
            runningTotal.PrintOn(framePrinter);
            framePrinter.EndPrint(this);
        }

        private void PrintThrowsOnPrinter(IFramePrinter framePrinter)
        {
            PrintThrowOnPrinter(throw1, framePrinter);
            PrintThrowOnPrinter(throw2, framePrinter);
        }

        private void PrintThrowOnPrinter(Throw aThrow, IThrowPrinter throwPrinter) =>
            IfThrowHasValueDo(aThrow, () => aThrow.PrintOn(throwPrinter));
    }
}
