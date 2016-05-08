using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class BonusTest
    {
        Frames frames;
        int targetFrameNumber;
        int numberOfThrowsRequired;
        Frame frame;
        Throw aThrow;
        IBonus bonus;

        [TestInitialize]
        public void Setup()
        {
            frames = Mock.Of<Frames>();
            targetFrameNumber = 1;
            numberOfThrowsRequired = 5;
            frame = Mock.Of<Frame>();
            aThrow = Mock.Of<Throw>();
            bonus = new Bonus(targetFrameNumber, numberOfThrowsRequired);
        }

        private void ContributeRequiredNumberOfThrows()
        {
            for (int i = 0; i < numberOfThrowsRequired; i++)
                bonus.ContributeThrowForFrame(aThrow, frame);
        }

        private void ContributeRequiredNumberOfThrowsFromSameFrame()
        {
            bonus.RequestFromFrames(frames);

            for (int i = 0; i < numberOfThrowsRequired; i++)
                bonus.ContributeThrowForFrame(aThrow, frame);
        }

        private void ContributeRequiredNumberOfThrowsFromSeparateFrames()
        {
            for (int i = 0; i < numberOfThrowsRequired; i++)
            {
                bonus.RequestFromFrames(frames);
                bonus.ContributeThrowForFrame(aThrow, frame);
            }
        }

        private void VerifyRequestsBonusFromFrameNumberToFrameNumber(FrameNumber fromFrameNumber, FrameNumber toFrameNumber)
        {
            Mock.Get(frames).Verify(frs => frs.RequestBonusFromFrameNumberToFrameNumber(fromFrameNumber, toFrameNumber));
        }

        [TestMethod]
        public void GivenTargetFrameNumber_WhenRequestingFromFrames_RequestsBonusFromFrameAfterTargetToTargetFrameNumber()
        {
            FrameNumber frameNumberAfterTarget = 2;

            bonus.RequestFromFrames(frames);

            VerifyRequestsBonusFromFrameNumberToFrameNumber(frameNumberAfterTarget, targetFrameNumber);
        }

        [TestMethod]
        public void GivenAThrowIsContributed_WhenRequestingFromFrames_RequestsBonusFromTwoFramesAfterTargetToTargetFrameNumber()
        {
            FrameNumber frameNumberTwoAfterTarget = 3;
            bonus.ContributeThrowForFrame(aThrow, frame);

            bonus.RequestFromFrames(frames);

            VerifyRequestsBonusFromFrameNumberToFrameNumber(frameNumberTwoAfterTarget, targetFrameNumber);
        }

        [TestMethod]
        public void Given2ThrowsAreContributed_WhenContributingMoreThrows_DoesNotAcceptMoreThanNumberOfThrowsRequired()
        {
            ContributeRequiredNumberOfThrows();

            // Contribute one more throw
            bonus.ContributeThrowForFrame(aThrow, frame);

            Mock.Get(frame).Verify(frs => frs.AcceptThrow(aThrow), Times.Exactly(numberOfThrowsRequired));
        }

        [TestMethod]
        public void GivenAllRequiredThrowsAreContributedFrom1Frame_WhenRequestingFromFrames_DoesNotRequestMoreThan1Bonus()
        {
            ContributeRequiredNumberOfThrowsFromSameFrame();

            bonus.RequestFromFrames(frames);

            Mock.Get(frames).Verify(frs => frs.RequestBonusFromFrameNumberToFrameNumber(It.IsAny<FrameNumber>(), It.IsAny<FrameNumber>()), Times.AtMostOnce());
        }

        [TestMethod]
        public void GivenAllRequiredThrowsAreContributedFromSeparateFrames_WhenRequestingFromFrames_DoesNotRequestMoreThanNumberOfThrowsRequired()
        {
            ContributeRequiredNumberOfThrowsFromSeparateFrames();

            bonus.RequestFromFrames(frames);

            Mock.Get(frames).Verify(frs => frs.RequestBonusFromFrameNumberToFrameNumber(It.IsAny<FrameNumber>(), It.IsAny<FrameNumber>()), Times.AtMost(numberOfThrowsRequired));
        }
    }
}
