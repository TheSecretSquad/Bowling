using Bowling.Printing;
using System;

namespace Bowling
{
    public class SampleScoreCardPrinter : IBowlingScoreCardPrinter
    {
        private int currentFrameCount = 0;
        private int currentFrameThrowCount = 0;

        public void BeginPrint(IBowlingScoreCard source) { }

        public void EndPrint(IBowlingScoreCard source) { }

        public void BeginPrint(Frames source) { }

        public void EndPrint(Frames source) { }

        public void BeginPrint(Frame source)
        {
            ++currentFrameCount;
            Console.WriteLine($"Frame {currentFrameCount}");
        }

        public void EndPrint(Frame source)
        {
            currentFrameThrowCount = 0;
            Console.WriteLine();
        }

        public void BeginPrint(Throw source)
        {
            ++currentFrameThrowCount;
        }

        public void EndPrint(Throw source) { }

        public void PrintThrow(int aThrow)
        {
            Console.WriteLine($"Throw {currentFrameThrowCount}: {aThrow}");
        }

        public void BeginPrint(RunningTotal source) { }

        public void EndPrint(RunningTotal source) { }

        public void PrintRunningTotal(int total)
        {
            Console.WriteLine($"Total: {total}");
        }
    }
}
