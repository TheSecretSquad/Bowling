using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingTest
{
    [TestClass]
    public class ValueTest
    {
        public class TestValue : Value<TestValue>
        {
            private int value;

            public TestValue(int value)
            {
                this.value = value;
            }

            public override int GetHashCode()
            {
                return value.GetHashCode();
            }
        }

        private Value<TestValue> value;

        [TestInitialize]
        public void Setup()
        {
            value = new TestValue(1);
        }

        [TestMethod]
        public void SameValuesAreEqual()
        {
            Assert.AreEqual(new TestValue(5), new TestValue(5));
        }

        [TestMethod]
        public void DifferentValuesAreNotEqual()
        {
            Assert.AreNotEqual(new TestValue(5), new TestValue(4));
        }

        [TestMethod]
        public void NotEqualToNull()
        {
            Assert.AreNotEqual(new TestValue(5), null);
        }

        [TestMethod]
        public void EqualsOperatorForDifferentInstancesWithSameValueIsTrue()
        {
            Assert.IsTrue(new TestValue(5) == new TestValue(5));
        }

        [TestMethod]
        public void EqualsOperatorForDifferentInstancesWithDifferentValueIsFalse()
        {
            Assert.IsFalse(new TestValue(5) == new TestValue(4));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithSameValueIsFalse()
        {
            Assert.IsFalse(new TestValue(5) != new TestValue(5));
        }

        [TestMethod]
        public void NotEqualsOperatorForDifferentInstancesWithDifferentValueIsTrue()
        {
            Assert.IsTrue(new TestValue(5) != new TestValue(4));
        }
    }
}
