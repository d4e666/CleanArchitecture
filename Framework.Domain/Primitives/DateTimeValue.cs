#region Usings

using System;
using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class DateTimeValue : Primitive<DateTime>
    {
        #region Constructors

        protected DateTimeValue(DateTime value) : base(value)
        {
        }

        #endregion
    }
}