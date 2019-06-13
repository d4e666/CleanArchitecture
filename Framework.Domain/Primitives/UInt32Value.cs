#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class UInt32Value : Primitive<uint>
    {
        #region Constructors

        protected UInt32Value(uint value) : base(value)
        {
        }

        #endregion
    }
}