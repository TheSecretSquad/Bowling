using Bowling;
using BowlingTest.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingTest
{
    [TestClass]
    public class ValueTest
    {
        private Value<SampleValue> value;

        [TestInitialize]
        public void Setup()
        {
            value = new SampleValue(1);
        }

        [TestMethod]
        public void SameValuesAreEqual()
        {
            Assert.AreEqual(new SampleValue(5), new SampleValue(5));
        }

        [TestMethod]
        public void DifferentValuesAreNotEqual()
        {
            Assert.AreNotEqual(new SampleValue(5), new SampleValue(4));
        }

        [TestMethod]
        public void NotEqualToNull()
        {
            Assert.AreNotEqual(new SampleValue(5), null);
        }

        [TestMethod]
        public void EqualsOperatorForDifferentInstancesWithSameValueIsTrue()
        {
            Assert.IsTrue(new SampleValue(5) == new SampleValue(5));
        }

        [TestMethod]
        public void EqualsOperatorForDifferentInstancesWithDifferentValueIsFalse()
        {
            Assert.IsFalse(new SampleValue(5) == new SampleValue(4));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithSameValueIsFalse()
        {
            Assert.IsFalse(new SampleValue(5) != new SampleValue(5));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithDifferentValueIsTrue()
        {
            Assert.IsTrue(new SampleValue(5) != new SampleValue(4));
        }
    }
}
