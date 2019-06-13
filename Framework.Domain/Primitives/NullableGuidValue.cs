#region Usings

using System;
using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableGuidValue : Primitive<Guid?>
    {
        #region Constructors

        protected NullableGuidValue(Guid? value) : base(value)
        {
        }

        #endregion
    }
}