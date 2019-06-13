#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class UInt64Value : Primitive<ulong>
    {
        #region Constructors

        protected UInt64Value(ulong value) : base(value)
        {
        }

        #endregion
    }
}