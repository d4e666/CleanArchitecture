#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class DoubleValue : Primitive<double>
    {
        #region Constructors

        protected DoubleValue(double value) : base(value)
        {
        }

        #endregion
    }
}