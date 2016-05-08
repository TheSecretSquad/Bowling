using Bowling;

namespace BowlingTest.TestHelpers
{
    public class SampleValue : Value<SampleValue>
    {
        private int value;

        public SampleValue(int value)
        {
            this.value = value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
