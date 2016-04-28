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

    }
}
