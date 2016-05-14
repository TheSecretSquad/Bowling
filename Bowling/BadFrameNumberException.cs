using System;

namespace Bowling
{
    [Serializable]
    public class BadFrameNumberException : Exception
    {
        public BadFrameNumberException()
        {
        }
    }
}
