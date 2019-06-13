#region Usings

using System;

#endregion

namespace Framework.Domain.Entities
{
    public abstract class AggregateRoot : Entity
    {
        #region Constructors

        /// <inheritdoc />
        protected AggregateRoot(Guid key) : base(key)
        {
        }

        #endregion
    }
}