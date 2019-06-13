#region Usings

using Framework.Domain.Primitives.Core;

#endregion

namespace Framework.Domain.Primitives
{
    public abstract class SingleValue : Primitive<float>
    {
        #region Constructors

        protected SingleValue(float value) : base(value)
        {
        }

        #endregion
    }
}