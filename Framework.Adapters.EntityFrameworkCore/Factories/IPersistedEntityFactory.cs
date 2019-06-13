namespace Framework.Adapters.EntityFrameworkCore.Factories
{
    public interface IPersistedEntityFactory<TPersistedEntity, TRecord>
    {
        #region Methods

        TPersistedEntity CreateFromPersistence(TRecord record);

        #endregion
    }
}