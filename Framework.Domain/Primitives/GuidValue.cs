#region Usings

using System;
using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class GuidValue : Primitive<Guid>
    {
        #region Constructors

        protected GuidValue(Guid value) : base(value)
        {
        }

        #endregion
    }
}