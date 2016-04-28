﻿using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BowlingTest
{
    public class TestScoreCardPrinter : IBowlingScoreCardPrinter
    {
        private static int ThrowIndex(int throwNumber) => throwNumber - 1;

        private static int FrameIndex(int frameNumber) => frameNumber - 1;

        protected IList<int> frameScores;
        protected ICollection<ICollection<int>> frameThrows;
        private bool printingThrow;
        private bool printingRunningTotal;

        public TestScoreCardPrinter()
        {
            this.frameScores = new List<int>();
            this.frameThrows = new List<ICollection<int>>();
        }

        public void VerifyHasNumberOfFrames(int numberOfFrames)
        {
            Assert.AreEqual(numberOfFrames, frameScores.Count());
        }

        public void VerifyFrameNumberHasScoreValue(int frameNumber, int scoreValue)
        {
            Assert.AreEqual(scoreValue, FrameScore(frameNumber));
        }

        public void VerifyFrameNumberThrowNumberHasValue(int frameNumber, int throwNumber, int throwValue)
        {
            Assert.AreEqual(throwValue, FrameThrowValue(frameNumber, throwNumber));
        }

        public void VerifyEachFrameHasNumberOfThrows(int throwValue)
        {
            foreach (ICollection<int> throws in frameThrows)
                Assert.AreEqual(throwValue, throws.Count);
        }

        public void VerifyLastSubsetOfFramesHasScoreValue(int lastSubsetCount, int scoreValue)
        {
            int skipCount = frameScores.Count() - lastSubsetCount;

            foreach (int frameScore in frameScores.Skip(skipCount))
                Assert.AreEqual(scoreValue, frameScore);
        }

        private int FrameScore(int frameNumber) =>
            frameScores.ElementAt(FrameIndex(frameNumber));

        private int FrameThrowValue(int frameNumber, int throwNumber) =>
            ThrowsForFrame(frameNumber).ElementAt(ThrowIndex(throwNumber));

        private ICollection<int> ThrowsForFrame(int frameNumber) =>
            frameThrows.ElementAt(FrameIndex(frameNumber));

        public void BeginPrint(IBowlingScoreCard source) { }

        public void EndPrint(IBowlingScoreCard source) { }

        public void BeginPrint(Frames source) { }

        public void EndPrint(Frames source) { }

        public void BeginPrint(Frame source)
        {
            frameScores.Add(0);
            frameThrows.Add(new List<int>());
        }

        public void EndPrint(Frame source) { }

        public void BeginPrint(Throw source) { printingThrow = true; }

        public void EndPrint(Throw source) { printingThrow = false; }

        private ICollection<int> CurrentFrameThrows() =>
            ThrowsForFrame(CurrentFrameNumber());

        private int CurrentFrameNumber() => frameScores.Count();

        public void BeginPrint(RunningTotal source) { printingRunningTotal = true; }

        public void EndPrint(RunningTotal source) { printingRunningTotal = false; }

        private void ScoreFrameNumberWithValue(int frameNumber, int total) =>
            frameScores[FrameIndex(frameNumber)] = total;

        public void PrintValue(int value)
        {
            if (printingThrow)
                CurrentFrameThrows().Add(value);

            if (printingRunningTotal)
                ScoreFrameNumberWithValue(CurrentFrameNumber(), value);
        }

        public void BeginPrint(PositiveInteger source) { }

        public void EndPrint(PositiveInteger source) { }
    }
}
