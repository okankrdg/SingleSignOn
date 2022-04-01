using IdentityServer4.Models;
using System.Collections.Generic;

namespace SingleSignOn;

public class IdentityConfig
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> { "role"}
            }
        };
    public static IEnumerable<ApiScope> ApiScopes =>
      new List<ApiScope> {
          new ("bookapi.read","Books Read"),
          new ("bookapi.write","Manage Books"),

          new ("userapi.read","Read users"),
          new ("userApi.write","Manage Users")
      };
    public static IEnumerable<ApiResource> ApiResources =>
     new List<ApiResource> {
         new ("bookapi", "Book Api")
         {
             Scopes = {"bookapi.read","bookapi.write"}
         },
         new ("userapi", "User Api")
         {
             Scopes = {"userapi.read", "userapi.write" },
             ApiSecrets = new List<Secret> {new ("SecretValue".Sha256())},
             UserClaims = new List<string> {"role"}
         },
     };
    public static IEnumerable<Client> Clients =>
    new List<Client> {
        new()
        {
            //Machine to machine
            ClientId = "book-store-m2m",
            ClientName = "Book Store client for m2m",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets =  {new("BookStore".Sha256())},
            AllowedScopes = {"userapi.read","userapi.write"}
        },
        new()
        {
            //Machine to machine
            ClientId = "book-store-app",
            ClientName = "Book Store App",
            AllowedGrantTypes = GrantTypes.Code,
            ClientSecrets = { new("BookStore".Sha256())},
            AllowedScopes = {"openid","userapi.read","profile"},
            RedirectUris = {"https://localhost:5105/signin-oidc"},
            FrontChannelLogoutUri = "https://localhost:5105/signin-oidc",
            PostLogoutRedirectUris = {"https://localhost:5105/signin-oidc"}
        }

    };
}

