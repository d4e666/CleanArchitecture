#region Usings

using Framework.Domain.Entities;

#endregion

namespace Framework.Domain.Factories
{
    public interface IEntityFactory<TEntity>
        where TEntity : Entity
    {
    }
}