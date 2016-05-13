using Bowling;
using Bowling.Printing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
// TODO: Refactor to add getters for testable values. I can write tests for getters to make sure they are returning
// the right data
namespace BowlingTest
{
    public class TestScoreCardPrinter : IBowlingScoreCardPrinter
    {
        private static int ThrowIndex(int throwNumber) => throwNumber - 1;

        private static int FrameIndex(int frameNumber) => frameNumber - 1;

        protected IList<int> frameScores;
        protected ICollection<ICollection<int>> frameThrows;

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

        public void VerifyEachFrameHasNumberOfThrows(int numberOfThrows)
        {
            foreach (ICollection<int> throws in frameThrows)
                Assert.AreEqual(numberOfThrows, throws.Count);
        }

        public void VerifyEachFrameNThroughMHasNumberOfThrows(int n, int m, int numberOfThrows)
        {
            for (int frameNumber = n; frameNumber <= m; frameNumber++)
                Assert.AreEqual(numberOfThrows, CountThrowsForFrame(frameNumber));
        }

        public void VerifyFrameNumberHasNumberOfThrows(int frameNumber, int numberOfThrows)
        {
            Assert.AreEqual(numberOfThrows, CountThrowsForFrame(frameNumber));
        }

        private int CountThrowsForFrame(int frameNumber) =>
            ThrowsForFrame(frameNumber).Count;

        //public void VerifyLastSubsetOfFramesHasScoreValue(int lastSubsetCount, int scoreValue)
        //{
        //    int skipCount = frameScores.Count() - lastSubsetCount;

        //    foreach (int frameScore in frameScores.Skip(skipCount))
        //        Assert.AreEqual(scoreValue, frameScore);
        //}

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

        public void BeginPrint(Throw source) { }

        public void EndPrint(Throw source) { }

        public void PrintThrow(int aThrow)
        {
            CurrentFrameThrows().Add(aThrow);
        }

        private ICollection<int> CurrentFrameThrows() =>
            ThrowsForFrame(CurrentFrameNumber());

        private int CurrentFrameNumber() => frameScores.Count();

        public void BeginPrint(RunningTotal source) { }

        public void EndPrint(RunningTotal source) { }

        public void PrintRunningTotal(int total)
        {
            ScoreFrameNumberWithValue(CurrentFrameNumber(), total);
        }

        private void ScoreFrameNumberWithValue(int frameNumber, int total) =>
            frameScores[FrameIndex(frameNumber)] = total;
    }
}
