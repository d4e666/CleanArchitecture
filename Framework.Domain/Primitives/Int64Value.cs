#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class Int64Value : Primitive<long>
    {
        #region Constructors

        protected Int64Value(long value) : base(value)
        {
        }

        #endregion
    }
}