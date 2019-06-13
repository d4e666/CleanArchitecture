#region Usings

using System;
using System.Runtime.Serialization;

#endregion

namespace Framework.Domain.Exceptions
{
    public class DomainException : Exception
    {
        #region Constructors

        /// <inheritdoc />
        public DomainException()
        {
        }

        /// <inheritdoc />
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        public DomainException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}