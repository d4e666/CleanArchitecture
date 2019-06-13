#region Usings

using System;
using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableDateTimeValue : Primitive<DateTime?>
    {
        #region Constructors

        protected NullableDateTimeValue(DateTime? value) : base(value)
        {
        }

        #endregion
    }
}