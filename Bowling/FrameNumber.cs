using System;
using Bowling.Printers;

namespace Bowling
{
    public class FrameNumber : Value<FrameNumber>
    {
        private const int FRAME_NUMBER_MAX_VALUE = 10;

        private readonly PositiveInteger value;

        public FrameNumber() : this(1) { }

        public FrameNumber(PositiveInteger value)
        {
            this.value = value;
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (value.AsInteger() <= 0 || value.AsInteger() > FRAME_NUMBER_MAX_VALUE)
                throw new BadFrameNumberException();
        }

        public virtual int AsInteger()
        {
            return value.AsInteger();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public void PrintOn(IFrameNumberPrinter printer)
        {
            printer.BeginPrint(this);
            value.PrintOn(printer);
            printer.EndPrint(this);
        }

        public static implicit operator FrameNumber(PositiveInteger pi)
        {
            return new FrameNumber(pi);
        }

        public static implicit operator FrameNumber(int i)
        {
            return new FrameNumber(i);
        }
    }
}
