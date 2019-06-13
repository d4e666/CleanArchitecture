#region Usings

using Framework.Domain.Entities;

#endregion

namespace Framework.Domain.Gateways
{
    public interface IEntityGateway<TEntity>
        where TEntity : AggregateRoot
    {
    }
}