namespace Bowling
{
    public class RunningTotal : Value<RunningTotal>
    {
        private readonly PositiveInteger total;

        public RunningTotal() : this(0) { }

        public RunningTotal(PositiveInteger total)
        {
            this.total = total;
        }

        public virtual RunningTotal AddThrow(Throw aThrow)
        {
            if(aThrow != null)
                return this + aThrow;

            return this;
        }

        public static implicit operator RunningTotal(PositiveInteger pi)
        {
            return new RunningTotal(pi);
        }

        public static implicit operator PositiveInteger(RunningTotal rt)
        {
            return rt.total;
        }

        public static implicit operator RunningTotal(int i)
        {
            return new RunningTotal(i);
        }

        public static implicit operator int(RunningTotal rt)
        {
            return rt.total;
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
