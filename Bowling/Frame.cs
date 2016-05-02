﻿using System;
using System.Linq;

namespace Bowling
{
    public class Frame
    {
        public static Frame Open() =>
            new Frame();

        public static Frame Strike(PositiveInteger frameNumber) =>
            new Frame(new Throw(10), Bonus.Strike(frameNumber), new RunningTotal());

        public static Frame TenthFrameStrike(Throw throw2, Throw throw3) =>
            new Frame(new Throw(10), throw2, throw3, new NoBonus(), new RunningTotal());

        public static Frame Spare(PositiveInteger frameNumber, Throw throw1) =>
            new Frame(throw1, new Throw(10 - throw1.AsInteger()), Bonus.Spare(frameNumber), new RunningTotal());

        public static Frame TenthFrameSpare(Throw throw1, Throw throw3) =>
            new Frame(throw1, new Throw(10 - throw1.AsInteger()), throw3, new NoBonus(), new RunningTotal());

        private Throw[] throws;
        private RunningTotal runningTotal;
        private IBonus bonus;
        private Frame nextFrame;

        public Frame()
                : this(new Throw[0], new NoBonus(), new RunningTotal()) { }

        public Frame(Throw throw1, IBonus bonus, RunningTotal runningTotal)
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
                throw new InvalidFrameException();
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
            foreach (Throw eachThrow in throws)
                frame.ContributeThrow(eachThrow);
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

            foreach (Throw eachThrow in throws)
                this.runningTotal = this.runningTotal.AddThrow(eachThrow);
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
