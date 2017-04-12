using System;

namespace Core
{
    public abstract class Entity<TIdentity, TState>
        where TState: EntityState<TIdentity>
    {
        public TIdentity Id => State.Id;

        protected TState State { get; }

        protected Entity(TState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            State = state;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(obj, null))
                return false;

            var objAsEntity = obj as Entity<TIdentity, TState>;
            if (objAsEntity == null)
                return false;

            return Id.Equals(objAsEntity.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}