using Bowling.Printing;
using System;

namespace Bowling
{
    public class Throw : Value<Throw>
    {
        private const int THROW_MAX_VALUE = 10;

        public static Throw Strike() =>
            new Throw(THROW_MAX_VALUE);

        public static Throw SpareDifferenceOf(Throw throw1) =>
            new Throw(THROW_MAX_VALUE - throw1.AsInteger());

        private readonly PositiveInteger value;

        public Throw() : this(0) { }

        public Throw(int value) : this(new PositiveInteger(value)) { }

        public Throw(PositiveInteger value)
        {
            this.value = value;
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (value.AsInteger() > THROW_MAX_VALUE)
                throw new BadThrowException();
        }

        public bool HasSpare()
        {
            return value.AsInteger() != THROW_MAX_VALUE;
        }

        public RunningTotal AsRunningTotal()
        {
            return new RunningTotal(value);
        }

        public virtual int AsInteger()
        {
            return value.AsInteger();
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
