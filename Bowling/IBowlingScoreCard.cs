using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public interface IBowlingScoreCard
    {
        void WriteFrame(Frame frame);

        void PrintOn(IBowlingScoreCardPrinter bowlingScoreCardPrinter);
    }
}
