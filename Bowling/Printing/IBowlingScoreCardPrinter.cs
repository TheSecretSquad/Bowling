namespace Bowling.Printing
{
    public interface IBowlingScoreCardPrinter : IPrinter<IBowlingScoreCard>, IFramesPrinter, IRunningTotalPrinter
    {
    }
}
