using System;

namespace Bowling
{
    public class SampleScoreCardPrinter : IBowlingScoreCardPrinter
    {
        private bool printingThrow;
        private bool printingRunningTotal;
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
            printingThrow = true;
            ++currentFrameThrowCount;
        }

        public void EndPrint(Throw source)
        {
            printingThrow = false;
        }

        public void BeginPrint(RunningTotal source)
        {
            printingRunningTotal = true;
        }

        public void EndPrint(RunningTotal source)
        {
            printingRunningTotal = false;
        }

        public void PrintPositiveIntValue(int value)
        {
            if(printingThrow)
                Console.WriteLine($"Throw {currentFrameThrowCount}: {value}");

            if (printingRunningTotal)
                Console.WriteLine($"Total: {value}");
        }

        public void BeginPrint(PositiveInteger source) { }

        public void EndPrint(PositiveInteger source) { }
    }
}
