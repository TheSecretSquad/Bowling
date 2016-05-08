namespace Bowling
{
    public class RunningTotal : Value<RunningTotal>
    {
        private readonly PositiveInteger total;

        public RunningTotal() : this(0) { }

        public RunningTotal(int total) : this(new PositiveInteger(total)) { }

        public RunningTotal(PositiveInteger total)
        {
            this.total = total;
        }

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

        public virtual RunningTotal AddTotal(PositiveInteger total)
        {
            return new RunningTotal(this.total.AsInteger() + total.AsInteger());
        }

        public virtual int AsInteger()
        {
            return total.AsInteger();
        }

        public virtual void PrintOn(IRunningTotalPrinter runningTotalPrinter)
        {
            runningTotalPrinter.BeginPrint(this);
            total.PrintOn(runningTotalPrinter);
            runningTotalPrinter.EndPrint(this);
        }

        public override int GetHashCode()
        {
            return total.GetHashCode();
        }
    }
}
