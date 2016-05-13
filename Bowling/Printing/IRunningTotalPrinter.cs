namespace Bowling.Printing
{
    public interface IRunningTotalPrinter : IPrinter<RunningTotal>
    {
        void PrintRunningTotal(int total);
    }
}