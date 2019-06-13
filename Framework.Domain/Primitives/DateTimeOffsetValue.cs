#region Usings

using System;
using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class DateTimeOffsetValue : Primitive<DateTimeOffset>
    {
        #region Constructors

        protected DateTimeOffsetValue(DateTimeOffset value) : base(value)
        {
        }

        #endregion
    }
}