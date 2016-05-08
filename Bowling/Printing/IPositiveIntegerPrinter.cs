namespace Bowling.Printing
{
    public interface IPositiveIntegerPrinter : IPrinter<PositiveInteger>
    {
        void PrintPositiveIntValue(int value);
    }
}
