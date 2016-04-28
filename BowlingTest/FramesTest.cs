using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class FramesTest
    {
        private Frames frames;
        private Frame frame;
        private IFramesPrinter framesPrinter;
        Frame frame1;
        Frame frame2;
        Frame frame3;

        [TestInitialize]
        public void Setup()
        {
            frame = Mock.Of<Frame>();
            framesPrinter = Mock.Of<IFramesPrinter>();
            frames = new Frames();
            frame1 = Mock.Of<Frame>();
            frame2 = Mock.Of<Frame>();
            frame3 = Mock.Of<Frame>();
        }

        [TestMethod]
        public void Given1FrameRecorded_WhenRecordingASecondFrame_InitializesSecondFrameWithFirstFrame()
        {
            frames.RecordFrame(frame1);

            frames.RecordFrame(frame2);

            Mock.Get(frame1).Verify(fr1 => fr1.InitializeNextFrame(frame2));
        }

        [TestMethod]
        public void Given2FramesRecorded_WhenRecordingAThirdFrame_InitializesThirdFrameWithSecondFrame()
        {
            frames.RecordFrame(frame1);
            frames.RecordFrame(frame2);

            frames.RecordFrame(frame3);

            Mock.Get(frame1).Verify(fr1 => fr1.InitializeNextFrame(frame2), Times.AtMostOnce());
            Mock.Get(frame2).Verify(fr2 => fr2.InitializeNextFrame(frame3));
        }

        [TestMethod]
        public void WhenRecordingFirstFrame_NoNextFrameInitializationIsDone()
        {
            frames.RecordFrame(frame1);

            Mock.Get(frame1).Verify(fr1 => fr1.InitializeNextFrame(It.IsAny<Frame>()), Times.Never);
        }

        [TestMethod]
        public void Given2FramesRecorded_WhenRequestingBonusesFromFrame2ToFrame1_ContributesBonusFromFrame2To1()
        {
            frames.RecordFrame(frame1);
            frames.RecordFrame(frame2);

            frames.RequestBonusFromFrameNumberToFrameNumber(2, 1);

            Mock.Get(frame1).Verify(f1 => f1.ContributeFromFrame(frame2));
        }

        [TestMethod]
        public void Given1FrameRecorded_WhenRequestingBonusesFromFrame2ToFrame1_DoesNotContributeBonusesForAnyFrames()
        {
            frames.RecordFrame(frame1);

            frames.RequestBonusFromFrameNumberToFrameNumber(2, 1);

            Mock.Get(frame1).Verify(f1 => f1.ContributeFromFrame(It.IsAny<Frame>()), Times.Never);
        }

        [TestMethod]
        public void WhenPrinting_BeginsAndEndsPrinting()
        {
            frames.PrintOn(framesPrinter);

            Mock.Get(framesPrinter).Verify(fsp => fsp.BeginPrint(frames));
            Mock.Get(framesPrinter).Verify(fsp => fsp.EndPrint(frames));
        }

        [TestMethod]
        public void WhenPrinting_PrintsEachFrame()
        {
            frames.RecordFrame(frame1);
            frames.RecordFrame(frame2);

            frames.PrintOn(framesPrinter);

            Mock.Get(frame1).Verify(f => f.PrintOn(framesPrinter));
            Mock.Get(frame2).Verify(f => f.PrintOn(framesPrinter));
        }
    }
}
