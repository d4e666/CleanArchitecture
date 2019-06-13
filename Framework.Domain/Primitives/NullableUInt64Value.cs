#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableUInt64Value : Primitive<ulong?>
    {
        #region Constructors

        protected NullableUInt64Value(ulong? value) : base(value)
        {
        }

        #endregion
    }
}