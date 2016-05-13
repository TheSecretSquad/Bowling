using Bowling.Printing;

namespace Bowling
{
    public class FrameNumber : Value<FrameNumber>
    {
        private const int LowestFrameNumber = 1;
        private const int HighestFrameNumber = 10;

        private readonly int frameNumber;

        public FrameNumber() : this(1) { }

        public FrameNumber(int value)
        {
            this.frameNumber = value;
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (!IsValid())
                throw new BadFrameNumberException();
        }

        private bool IsValid() =>
            frameNumber >= LowestFrameNumber && frameNumber <= HighestFrameNumber;

        public virtual int AsInteger()
        {
            return frameNumber;
        }

        public override int GetHashCode()
        {
            return frameNumber.GetHashCode();
        }

        public virtual FrameNumber Next()
        {
            return IsMaxFrameNumber() ? this : NextFrameNumber();
        }

        private bool IsMaxFrameNumber() =>
            AsInteger() == HighestFrameNumber;

        private FrameNumber NextFrameNumber() =>
            new FrameNumber(frameNumber + 1);

        public void PrintOn(IFrameNumberPrinter printer)
        {
            printer.BeginPrint(this);
            printer.PrintFrameNumber(frameNumber);
            printer.EndPrint(this);
        }
    }
}
