using Bowling.Printing;

namespace Bowling
{
    public interface IBowlingScoreCard
    {
        void WriteFrame(Frame frame);

        void PrintOn(IBowlingScoreCardPrinter bowlingScoreCardPrinter);
    }
}
