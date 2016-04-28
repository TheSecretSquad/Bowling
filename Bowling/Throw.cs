namespace Bowling
{
    public class Throw : Value<Throw>
    {
        private readonly PositiveInteger value;

        public Throw() : this(0) { }

        public Throw(int value) : this(new PositiveInteger(value)) { }

        public Throw(PositiveInteger value)
        {
            this.value = value;
            ValidateValue();
        }

        public virtual RunningTotal AsRunningTotal()
        {
            return new RunningTotal(value.AsInteger());
        }

        private void ValidateValue()
        {
            if (value.AsInteger() > 10)
                throw new BadThrowException();
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
