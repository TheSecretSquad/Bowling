using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// TODO: Write tests for this
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
