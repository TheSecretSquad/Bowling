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

        public static implicit operator Throw(PositiveInteger pi)
        {
            return new Throw(pi);
        }

        public static implicit operator PositiveInteger(Throw t)
        {
            return (int)t;
        }

        public static implicit operator Throw(int i)
        {
            return new Throw(i);
        }

        public static implicit operator int(Throw t)
        {
            return AsInt(t);
        }

        private static int AsInt(Throw t) =>
            t?.value ?? default(int);

        public static implicit operator RunningTotal(Throw t)
        {
            return new RunningTotal(t);
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
