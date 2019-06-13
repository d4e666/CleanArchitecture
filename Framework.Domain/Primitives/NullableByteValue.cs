#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class NullableByteValue : Primitive<byte?>
    {
        #region Constructors

        protected NullableByteValue(byte? value) : base(value)
        {
        }

        #endregion
    }
}