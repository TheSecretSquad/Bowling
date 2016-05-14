using Bowling.Printing;

namespace Bowling
{
    public class BowlingScoreCard : IBowlingScoreCard
    {
        private Frames frames;
        private Bonuses bonuses;

        public BowlingScoreCard(Frames frames, Bonuses bonuses)
        {
            this.frames = frames;
            this.bonuses = bonuses;
        }

        public virtual void WriteFrame(Frame frame)
        {
            frames.RecordFrame(frame);
            bonuses.UpdateWithFramesNewFrame(frames, frame);
        }

        public virtual void PrintOn(IBowlingScoreCardPrinter bowlingScoreCardPrinter)
        {
            bowlingScoreCardPrinter.BeginPrint(this);
            frames.PrintOn(bowlingScoreCardPrinter);
            bowlingScoreCardPrinter.EndPrint(this);
        }
    }
}
