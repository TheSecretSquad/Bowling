using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingTest.TestHelperTest
{
    [TestClass]
    public class SampleGameFrameFactoryTest
    {
        private SampleGameFrameFactory frameFactory;

        [TestInitialize]
        public void Setup()
        {
            frameFactory = new SampleGameFrameFactory();
        }

        [TestMethod]
        public void CreatesStrike()
        {
            Frame strike = frameFactory.Strike(5);
            Assert.IsNotNull(strike);
        }

        [TestMethod]
        public void CreatesOpenFrame()
        {
            Frame open = frameFactory.Open();
            Assert.IsNotNull(open);
        }

        [TestMethod]
        public void CreatesSpareFrame()
        {
            Frame spare = frameFactory.Spare(5, new Throw(5));
            Assert.IsNotNull(spare);
        }

        [TestMethod]
        public void CreatesTenthFrameStrike()
        {
            Frame tenthFrameStrike = frameFactory.TenthFrameStrike(new Throw(4), new Throw(5));
            Assert.IsNotNull(tenthFrameStrike);
        }

        [TestMethod]
        public void CreatesTenthFrameSpare()
        {
            Frame tenthFrameSpare = frameFactory.TenthFrameSpare(new Throw(4), new Throw(5));
            Assert.IsNotNull(tenthFrameSpare);
        }
    }
}
