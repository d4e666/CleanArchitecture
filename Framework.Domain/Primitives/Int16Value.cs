#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class Int16Value : Primitive<short>
    {
        #region Constructors

        protected Int16Value(short value) : base(value)
        {
        }

        #endregion
    }
}