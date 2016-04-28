using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BowlingTest
{
    [TestClass]
    public class BonusesTest
    {
        private Bonuses bonuses;
        private Frames frames;
        private Frame frame;

        [TestInitialize]
        public void Setup()
        {
            frames = Mock.Of<Frames>();
            frame = Mock.Of<Frame>();
            bonuses = new Bonuses();
        }

        [TestMethod]
        public void WhenUpdatingAnnouncesNewFrameToBonuses()
        {
            Frames unusedFrames = Mock.Of<Frames>();

            bonuses.UpdateWithFramesNewFrame(unusedFrames, frame);

            Mock.Get(frame).Verify(fr => fr.AnnounceToBonuses(bonuses));
        }

        [TestMethod]
        public void WhenUpdatingRequestsBonusesForExistingBonuses()
        {
            IBonus bonus1 = Mock.Of<IBonus>();
            IBonus bonus2 = Mock.Of<IBonus>();
            bonuses.RememberBonus(bonus1);
            bonuses.RememberBonus(bonus2);

            bonuses.UpdateWithFramesNewFrame(frames, frame);

            Mock.Get(bonus1).Verify(bs1 => bs1.RequestFromFrames(frames));
            Mock.Get(bonus2).Verify(bs2 => bs2.RequestFromFrames(frames));
        }
    }
}
