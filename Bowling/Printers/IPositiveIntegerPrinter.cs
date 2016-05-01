namespace Bowling
{
    public interface IPositiveIntegerPrinter : IPrinter<PositiveInteger>
    {
        void PrintValue(int value);
    }
}
