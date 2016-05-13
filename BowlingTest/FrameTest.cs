using Bowling;
using Bowling.Printing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class FrameTest
    {
        private Frame frame;
        private Frame otherFrame;
        private Throw otherThrow;
        private Throw throw1;
        private Throw throw2;
        private RunningTotal runningTotal;
        private RunningTotal bonusTotal;
        private Bonuses bonuses;
        private IBonus bonus;
        private IFramePrinter framePrinter;
        private Frame nextFrame;

        [TestInitialize]
        public void Setup()
        {
            framePrinter = Mock.Of<IFramePrinter>();
            throw1 = Mock.Of<Throw>();
            throw2 = Mock.Of<Throw>();
            runningTotal = Mock.Of<RunningTotal>();
            Mock.Get(runningTotal).Setup(rt => rt.AddThrow(It.IsAny<Throw>())).Returns(runningTotal);
            bonusTotal = Mock.Of<RunningTotal>();
            bonuses = Mock.Of<Bonuses>();
            bonus = Mock.Of<IBonus>();
            otherFrame = Mock.Of<Frame>();
            otherThrow = Mock.Of<Throw>();
            nextFrame = Mock.Of<Frame>();
            frame = new Frame(throw1, throw2, bonus, runningTotal);
            frame.InitializeNextFrame(nextFrame);
        }

        [TestMethod]
        public void WhenConstructed_StartsRunningTotalWithGivenThrowValues()
        {
            new Frame(throw1, throw2, bonus, runningTotal);

            Mock.Get(runningTotal).Verify(rt => rt.AddThrow(throw1));
            Mock.Get(runningTotal).Verify(rt => rt.AddThrow(throw2));
        }

        [TestMethod]
        [ExpectedException(typeof(BadFrameException))]
        public void WhenConstructed_CannotContainNullThrows()
        {
            new Frame(new Throw[1] { null }, bonus, runningTotal);
        }

        [TestMethod]
        [ExpectedException(typeof(BadFrameException))]
        public void WhenSpareConstructed_FailsIfThrow1DoesNotHaveSpareAvailable()
        {
            Frame.Spare(new FrameNumber(1), Throw.Strike());
        }

        [TestMethod]
        public void WhenAnnouncingBonuses_RemembersFrameBonus()
        {
            frame.AnnounceToBonuses(bonuses);

            Mock.Get(bonuses).Verify(bs => bs.RememberBonus(bonus));
        }

        [TestMethod]
        public void WhenReceivingContributionFromAnotherFrame_ContributedFrameContributesToReceivingFrame()
        {
            frame.ContributeFromFrame(otherFrame);

            Mock.Get(otherFrame).Verify(of => of.ContributeToFrame(frame));
        }

        [TestMethod]
        public void WhenContributingToAnotherFrame_ContributesThrowsToOtherFrame()
        {
            frame.ContributeToFrame(otherFrame);

            Mock.Get(otherFrame).Verify(of => of.ContributeThrow(throw1));
            Mock.Get(otherFrame).Verify(of => of.ContributeThrow(throw2));
        }

        [TestMethod]
        public void WhenContributingAThrow_UpdatesTheFrameBonus()
        {
            frame.ContributeThrow(otherThrow);

            Mock.Get(bonus).Verify(b => b.ContributeThrowForFrame(otherThrow, frame));
        }

        [TestMethod]
        public void WhenAcceptingABonusThrow_AddsThrowToRunningTotal()
        {
            frame.AcceptThrow(otherThrow);

            Mock.Get(runningTotal).Verify(rt => rt.AddThrow(otherThrow));
        }

        [TestMethod]
        public void GivenAFrameRefersToItsNextFrame_WhenAcceptingABonusThrow_StartsNextFrameAtNewRunningTotal()
        {
            frame.AcceptThrow(otherThrow);

            Mock.Get(nextFrame).Verify(nf => nf.RebaseWithRunningTotal(It.IsAny<RunningTotal>()));
        }

        [TestMethod]
        public void GivenAFrameDoesNotPointToItsNextFrame_WhenAcceptingABonusThrow_DoesNotInteractWithNextFrame()
        {
            Frame frameWithNoNextFrame = new Frame(throw1, throw2, bonus, runningTotal);
            // Next frame not initialized
            frameWithNoNextFrame.AcceptThrow(otherThrow);
            // Will fail on null reference
        }

        [TestMethod]
        public void WhenInitializingTheNextFrame_StartsNextFrameWithPreviousFrameRunningTotal()
        {
            RunningTotal previousRunningTotal = runningTotal;

            frame.InitializeNextFrame(otherFrame);
            
            Mock.Get(otherFrame).Verify(of => of.RebaseWithRunningTotal(previousRunningTotal));
        }

        [TestMethod]
        public void WhenRebasingRunningTotal_RecalculatesRunningTotalWithNewBaseAndExistingThrows()
        {
            RunningTotal otherRunningTotal = Mock.Of<RunningTotal>();
            Mock.Get(otherRunningTotal).Setup(rt => rt.AddThrow(It.IsAny<Throw>())).Returns(otherRunningTotal);

            frame.RebaseWithRunningTotal(otherRunningTotal);

            Mock.Get(otherRunningTotal).Verify(ort => ort.AddThrow(throw1));
            Mock.Get(otherRunningTotal).Verify(ort => ort.AddThrow(throw2));
        }

        [TestMethod]
        public void WhenPrinting_BeginsAndEndsPrinting()
        {
            frame.PrintOn(framePrinter);
            Mock.Get(framePrinter).Verify(scp => scp.BeginPrint(frame));
            Mock.Get(framePrinter).Verify(scp => scp.EndPrint(frame));
        }

        [TestMethod]
        public void WhenPrinting_PrintsEachThrow()
        {
            frame.PrintOn(framePrinter);

            Mock.Get(throw1).Verify(t1 => t1.PrintOn(framePrinter));
            Mock.Get(throw2).Verify(t1 => t1.PrintOn(framePrinter));
        }

        [TestMethod]
        public void WhenPrinting_PrintsRunningTotal()
        {
            frame.PrintOn(framePrinter);

            Mock.Get(runningTotal).Verify(rt => rt.PrintOn(framePrinter));
        }
    }
}
