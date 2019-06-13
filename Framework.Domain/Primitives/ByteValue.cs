#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class ByteValue : Primitive<byte>
    {
        #region Constructors

        protected ByteValue(byte value) : base(value)
        {
        }

        #endregion
    }
}