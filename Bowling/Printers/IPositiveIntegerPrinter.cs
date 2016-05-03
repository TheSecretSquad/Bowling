namespace Bowling
{
    public interface IPositiveIntegerPrinter : IPrinter<PositiveInteger>
    {
        void PrintPositiveIntValue(int value);
    }
}
