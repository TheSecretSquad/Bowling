using Bowling.Printing;

namespace Bowling
{
    public class FrameNumber : Value<FrameNumber>
    {
        private const int FRAME_NUMBER_MIN_VALUE = 1;
        private const int FRAME_NUMBER_MAX_VALUE = 10;

        private readonly PositiveInteger value;

        public FrameNumber() : this(1) { }

        public FrameNumber(int value) : this(new PositiveInteger(value)) { }

        public FrameNumber(PositiveInteger value)
        {
            this.value = value;
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (!IsValid())
                throw new BadFrameNumberException();
        }

        private bool IsValid() =>
            AsInteger() >= FRAME_NUMBER_MIN_VALUE && AsInteger() <= FRAME_NUMBER_MAX_VALUE;

        public virtual int AsInteger()
        {
            return value.AsInteger();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public virtual FrameNumber Next()
        {
            return IsMaxFrameNumber() ? this : NextFrameNumber();
        }

        private bool IsMaxFrameNumber() =>
            AsInteger() == FRAME_NUMBER_MAX_VALUE;

        private FrameNumber NextFrameNumber() =>
            new FrameNumber(AsInteger() + 1);

        public void PrintOn(IFrameNumberPrinter printer)
        {
            printer.BeginPrint(this);
            value.PrintOn(printer);
            printer.EndPrint(this);
        }
    }
}
