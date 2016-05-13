using Bowling.Printing;
using System;

namespace Bowling
{
    public class Throw : Value<Throw>
    {
        private const int MinThrowValue = 0;
        private const int MaxThrowValue = 10;

        public static Throw Strike() =>
            new Throw(MaxThrowValue);

        public static Throw SpareDifferenceOf(Throw throw1) =>
            new Throw(MaxThrowValue - throw1.AsInteger());

        private readonly int throwValue;

        public Throw() : this(0) { }

        public Throw(int throwValue)
        {
            this.throwValue = throwValue;
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (!IsInValidRange())
                throw new BadThrowException();
        }

        private bool IsInValidRange() =>
            throwValue >= MinThrowValue && throwValue <= MaxThrowValue;

        public virtual bool HasSpare()
        {
            return throwValue != MaxThrowValue;
        }

        public virtual RunningTotal AsRunningTotal()
        {
            return new RunningTotal(throwValue);
        }

        public virtual int AsInteger()
        {
            return throwValue;
        }

        public override int GetHashCode()
        {
            return throwValue.GetHashCode();
        }

        public virtual void PrintOn(IThrowPrinter printer)
        {
            printer.BeginPrint(this);
            printer.PrintThrow(throwValue);
            printer.EndPrint(this);
        }
    }
}
