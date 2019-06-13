namespace Framework.Domain.Services.Identity
{
    public abstract class IdentityProvider<TIdentity> : IIdentityProvider<TIdentity>
    {
        #region Methods

        /// <inheritdoc />
        public abstract TIdentity Create();

        #endregion
    }
}