#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class SByteValue : Primitive<sbyte>
    {
        #region Constructors

        protected SByteValue(sbyte value) : base(value)
        {
        }

        #endregion
    }
}