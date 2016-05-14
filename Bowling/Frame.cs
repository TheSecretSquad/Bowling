using Bowling.Printing;
using System;
using System.Linq;

namespace Bowling
{
    public class Frame
    {
        public static Frame Open() =>
            new Frame();

        public static Frame Strike(FrameNumber frameNumber) =>
            new Frame(Throw.Strike(), Bonus.Strike(frameNumber), new RunningTotal());

        public static Frame TenthFrameStrike(Throw throw2, Throw throw3) =>
            new Frame(Throw.Strike(), throw2, throw3, new NoBonus(), new RunningTotal());

        public static Frame Spare(FrameNumber frameNumber, Throw throw1)
        {
            if(!throw1.HasSpare())
                throw new BadFrameException();

            return new Frame(throw1, Throw.SpareDifferenceOf(throw1), Bonus.Spare(frameNumber), new RunningTotal());
        }

        public static Frame TenthFrameSpare(Throw throw1, Throw throw3) =>
            new Frame(throw1, Throw.SpareDifferenceOf(throw1), throw3, new NoBonus(), new RunningTotal());

        private Throw[] throws;
        private RunningTotal runningTotal;
        private IBonus bonus;
        private Frame nextFrame;

        public Frame()
                : this(new Throw[2] { new Throw(0), new Throw(0) }, new NoBonus(), new RunningTotal()) { }

        private Frame(Throw throw1, IBonus bonus, RunningTotal runningTotal)
                : this(new Throw[1] { throw1 }, bonus, runningTotal) { }

        public Frame(Throw throw1, Throw throw2, IBonus bonus, RunningTotal runningTotal)
                : this(new Throw[2] { throw1, throw2 }, bonus, runningTotal) { }

        public Frame(Throw throw1, Throw throw2, Throw throw3, IBonus bonus, RunningTotal runningTotal)
                : this(new Throw[3] { throw1, throw2, throw3 }, bonus, runningTotal) { }

        public Frame(Throw[] throws, IBonus bonus, RunningTotal runningTotal)
        {
            this.throws = throws;
            this.runningTotal = runningTotal;
            this.bonus = bonus;
            this.nextFrame = null;

            StartRunningTotal();

            if (this.throws.Contains(null))
                throw new BadFrameException();
        }

        private void StartRunningTotal() =>
            RebaseWithRunningTotal(runningTotal);

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
            EachThrowDo(aThrow => frame.ContributeThrow(aThrow));
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

        public virtual void InitializeNextFrame(Frame frame)
        {
            nextFrame = frame;
            RestartNextFrameTotal();
        }

        private void RestartNextFrameTotal()
        {
            if (nextFrame != null)
                nextFrame.RebaseWithRunningTotal(runningTotal);
        }

        public virtual void RebaseWithRunningTotal(RunningTotal runningTotal)
        {
            this.runningTotal = runningTotal;
            EachThrowDo(aThrow => this.runningTotal = this.runningTotal.AddThrow(aThrow));
        }

        private void EachThrowDo(Action<Throw> throwAction)
        {
            foreach (Throw eachThrow in throws)
                throwAction(eachThrow);
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
            foreach (Throw eachThrow in throws)
                eachThrow.PrintOn(framePrinter);
        }
    }
}
