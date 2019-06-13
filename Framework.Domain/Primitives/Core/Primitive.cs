#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Framework.Domain.Primitives.Core
{
    public abstract class Primitive<TValue> : IEquatable<Primitive<TValue>>
    {
        #region Fields

        private readonly TValue _value;

        #endregion

        #region Constructors

        protected Primitive(TValue value)
        {
            this._value = value;
        }

        #endregion

        #region Methods

        public static implicit operator TValue(Primitive<TValue> primitive)
        {
            return primitive._value;
        }

        public static bool operator ==(Primitive<TValue> left, Primitive<TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Primitive<TValue> left, Primitive<TValue> right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(this._value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this._value?.ToString() ?? string.Empty;
        }

        /// <inheritdoc />
        public bool Equals(Primitive<TValue> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!this.EqualType(this.GetType(), other.GetType()))
                return false;

            return this.EqualValue(this._value, other._value);
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

            return this.Equals((Primitive<TValue>) other);
        }

        protected virtual bool EqualType(Type thisType, Type otherType)
        {
            return thisType == otherType;
        }

        protected virtual bool EqualValue(TValue thisValue, TValue otherValue)
        {
            return EqualityComparer<TValue>.Default.Equals(thisValue, otherValue);
        }

        #endregion
    }
}