namespace Bowling.Printing
{
    public interface IThrowPrinter : IPrinter<Throw>
    {
        void PrintThrow(int aThrow);
    }
}