namespace Bowling
{
    public class Throw : Value<Throw>
    {
        private readonly PositiveInteger value;

        public Throw() : this(0) { }

        public Throw(PositiveInteger value)
        {
            this.value = value;
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (value > 10)
                throw new BadThrowException();
        }

        public RunningTotal AsRunningTotal()
        {
            return new RunningTotal(value);
        }

        public PositiveInteger AsInteger()
        {
            return value;
        }

        public static implicit operator RunningTotal(Throw t)
        {
            return new RunningTotal(t.AsInteger());
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public virtual void PrintOn(IThrowPrinter throwPrinter)
        {
            throwPrinter.BeginPrint(this);
            value.PrintOn(throwPrinter);
            throwPrinter.EndPrint(this);
        }
    }
}
