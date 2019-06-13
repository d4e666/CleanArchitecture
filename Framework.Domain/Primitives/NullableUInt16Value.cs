#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableUInt16Value : Primitive<ushort?>
    {
        #region Constructors

        protected NullableUInt16Value(ushort? value) : base(value)
        {
        }

        #endregion
    }
}