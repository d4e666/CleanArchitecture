namespace Framework.Adapters.Persistence.Database.NetStandard
{
    public interface IUnitOfWorkProvider
    {
        #region Methods

        T Create<T>()
            where T : IUnitOfWork;

        #endregion
    }
}