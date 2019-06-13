namespace Framework.Domain.Services.Identity
{
    public interface IIdentityProvider<TIdentity>
    {
        #region Methods

        TIdentity Create();

        #endregion
    }
}