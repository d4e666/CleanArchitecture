#region Usings

using System;

#endregion

namespace Framework.Domain.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
        #region Constructors

        protected Entity(Guid key)
        {
            this.Key = key;
        }

        #endregion

        #region Properties

        public Guid Key { get; }

        #endregion

        #region Methods

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!this.EqualType(this.GetType(), other.GetType()))
                return false;

            return this.Key.Equals(other.Key);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!this.EqualType(this.GetType(), other.GetType()))
                return false;

            return this.Equals((Entity) other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        protected virtual bool EqualType(Type thisType, Type otherType)
        {
            return thisType == otherType;
        }

        #endregion
    }
}