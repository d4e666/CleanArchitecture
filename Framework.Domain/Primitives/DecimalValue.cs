#region Usings

#endregion

#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class DecimalValue : Primitive<decimal>
    {
        #region Constructors

        protected DecimalValue(decimal value) : base(value)
        {
        }

        #endregion
    }
}