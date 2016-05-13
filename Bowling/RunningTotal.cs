using Bowling.Printing;
using System;

namespace Bowling
{
    public class RunningTotal : Value<RunningTotal>
    {
        private const int MinimumTotal = 0;
        private readonly int total;

        public RunningTotal() : this(0) { }

        public RunningTotal(int total)
        {
            this.total = total;
            Validate();
        }

        private void Validate()
        {
            if (!IsPositive())
                throw new NegativeRunningTotalException();
        }

        private bool IsPositive() =>
            total >= MinimumTotal;

        public virtual RunningTotal AddThrow(Throw aThrow)
        {
            if (aThrow != null)
                return AddRunningTotal(aThrow.AsRunningTotal());

            return this;
        }

        public virtual RunningTotal AddRunningTotal(RunningTotal otherRunningTotal)
        {
            return otherRunningTotal.AddTotal(total);
        }

        public virtual RunningTotal AddTotal(int total)
        {
            return new RunningTotal(this.total + total);
        }

        public virtual int AsInteger()
        {
            return total;
        }

        public virtual void PrintOn(IRunningTotalPrinter runningTotalPrinter)
        {
            runningTotalPrinter.BeginPrint(this);
            runningTotalPrinter.PrintRunningTotal(total);
            runningTotalPrinter.EndPrint(this);
        }

        public override int GetHashCode()
        {
            return total.GetHashCode();
        }
    }
}
