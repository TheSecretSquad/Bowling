namespace Bowling.Printing
{
    public interface IFrameNumberPrinter : IPrinter<FrameNumber>
    {
        void PrintFrameNumber(int frameNumber);
    }
}
