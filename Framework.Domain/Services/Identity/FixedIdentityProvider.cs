namespace Framework.Domain.Services.Identity
{
    public class FixedIdentityProvider<TIdentity> : IdentityProvider<TIdentity>
    {
        private readonly TIdentity _value;

        public FixedIdentityProvider(TIdentity value)
        {
            this._value = value;
        }

        /// <inheritdoc />
        public override TIdentity Create()
        {
            return this._value;
        }
    }
}