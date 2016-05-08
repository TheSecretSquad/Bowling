using Bowling.Printing;

namespace Bowling
{
    public class PositiveInteger : Value<PositiveInteger>
    {
        private readonly int value;

        public PositiveInteger() : this(0) { }

        public PositiveInteger(int value)
        {
            this.value = value;
            Validate();
        }

        public PositiveInteger(PositiveInteger other)
        {
            this.value = other.value;
        }

        private void Validate()
        {
            if (value < 0)
                throw new NegativeValueException();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public virtual void PrintOn(IPositiveIntegerPrinter positiveIntegerPrinter)
        {
            positiveIntegerPrinter.BeginPrint(this);
            positiveIntegerPrinter.PrintPositiveIntValue(value);
            positiveIntegerPrinter.EndPrint(this);
        }

        public virtual int AsInteger()
        {
            return value;
        }

        public static PositiveInteger operator ++(PositiveInteger pi)
        {
            return new PositiveInteger(pi.value + 1);
        }
    }
}
