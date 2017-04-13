using System;

namespace Infrastructure
{
    public abstract class Identity<T>
    {
        public T Value { get; }

        protected Identity(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public override bool Equals(object obj)
        {
            var iObj = obj as Identity<T>;
            if (iObj != null)
                return Value.Equals(iObj.Value);

            if (obj is T)
                return Value.Equals(obj);

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator T(Identity<T> identity)
        {
            return identity.Value;
        }
    }
}