using System;
using System.Collections.Generic;

namespace Rehearsal.Infrastructure
{
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(obj, null))
                return false;

            if (GetType() != obj.GetType())
                return false;

            foreach (var selectProperty in GetCompareProperties())
            {
                var a = selectProperty((T)this);
                var b = selectProperty((T)obj);

                if (a == null) { 
                    if (b != null)
                        return false;
                }
                else if (!a.Equals(b))
                    return false;
            }

            return true;
        }

        protected abstract IEnumerable<Func<T, object>> GetCompareProperties();

        public static bool operator ==(ValueObject<T> c1, ValueObject<T> c2)
        {
            if (ReferenceEquals(c1, null)) return ReferenceEquals(c2, null);

            return c1.Equals(c2);
        }

        public static bool operator !=(ValueObject<T> c1, ValueObject<T> c2)
        {
            return !(c1 == c2);
        }

        public override int GetHashCode()
        {
            var hashCode = 17;
            const int multiplier = 59;

            foreach (var selectProperty in GetCompareProperties())
            {
                var value = selectProperty((T)this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }
    }
}