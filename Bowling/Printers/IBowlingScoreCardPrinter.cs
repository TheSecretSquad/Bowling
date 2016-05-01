namespace Bowling
{
    public interface IBowlingScoreCardPrinter : IPrinter<IBowlingScoreCard>, IFramesPrinter, IRunningTotalPrinter
    {
    }
}
