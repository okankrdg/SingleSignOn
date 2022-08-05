using IdentityServer4.Models;

namespace SingleSignOn.IdentityServer
{
    public class IdentityServerExtensions
    {
        public static IEnumerable<Client> Clients =>
   new List<Client>
   {
        new Client
        {
            ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            // scopes that client has access to
            AllowedScopes = { "api1" }
        }
   };
    }

}
