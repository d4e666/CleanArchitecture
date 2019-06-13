#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Framework.Domain.Entities
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        #region Methods

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public bool Equals(ValueObject other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!this.EqualType(this.GetType(), other.GetType()))
                return false;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
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

            return this.Equals((ValueObject) other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = new HashCode();

            foreach (var component in this.GetEqualityComponents())
                hash.Add(component);

            return hash.ToHashCode();
        }

        protected virtual bool EqualType(Type thisType, Type otherType)
        {
            return thisType == otherType;
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        #endregion
    }
}