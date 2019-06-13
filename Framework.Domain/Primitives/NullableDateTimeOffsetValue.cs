#region Usings

using System;
using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableDateTimeOffsetValue : Primitive<DateTimeOffset?>
    {
        #region Constructors

        protected NullableDateTimeOffsetValue(DateTimeOffset? value) : base(value)
        {
        }

        #endregion
    }
}