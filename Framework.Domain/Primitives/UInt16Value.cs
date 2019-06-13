#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class UInt16Value : Primitive<ushort>
    {
        #region Constructors

        protected UInt16Value(ushort value) : base(value)
        {
        }

        #endregion
    }
}