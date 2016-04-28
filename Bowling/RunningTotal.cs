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
            if(aThrow != null)
                return AddRunningTotal(aThrow.AsRunningTotal());

            return this;
        }

        public virtual RunningTotal AddRunningTotal(RunningTotal otherRunningTotal)
        {
            return otherRunningTotal.AddTotal(total.AsInteger());
        }

        public virtual RunningTotal AddTotal(int total)
        {
            return new RunningTotal(this.total.AsInteger() + total);
        }

        public virtual RunningTotal RestartFromTotalWithThrows(RunningTotal otherRunningTotal, Throw throw1, Throw throw2)
        {
            return otherRunningTotal.AddThrow(throw1).AddThrow(throw2);
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
