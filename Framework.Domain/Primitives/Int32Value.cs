#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class Int32Value : Primitive<int>
    {
        #region Constructors

        protected Int32Value(int value) : base(value)
        {
        }

        #endregion
    }
}