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

        private void Validate()
        {
            if (value < 0)
                throw new NegativeValueException();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public int AsInteger()
        {
            return value;
        }

        public virtual void PrintOn(IPositiveIntegerPrinter positiveIntegerPrinter)
        {
            positiveIntegerPrinter.BeginPrint(this);
            positiveIntegerPrinter.PrintValue(value);
            positiveIntegerPrinter.EndPrint(this);
        }
    }
}
