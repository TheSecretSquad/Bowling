using System;

namespace Bowling
{
    public abstract class Value<T> : IEquatable<T> where T : class, IEquatable<T>
    {
        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public bool Equals(T other)
        {
            if (other == null)
                return false;

            return GetHashCode().Equals(other.GetHashCode());
        }

        public static bool operator ==(Value<T> obj1, Value<T> obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;

            if ((object)obj1 == null || (object)obj2 == null)
                return false;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Value<T> obj1, Value<T> obj2)
        {
            return !(obj1 == obj2);
        }

        public abstract override int GetHashCode();
    }
}
