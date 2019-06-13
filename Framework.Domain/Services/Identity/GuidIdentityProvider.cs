using System;

namespace Framework.Domain.Services.Identity
{
    public class GuidIdentityProvider : IdentityProvider<Guid> {
        /// <inheritdoc />
        public override Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}