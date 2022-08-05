using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace SingleSignOn;

public class IdentityConfig
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResource()
            {
                Name="roles",
                UserClaims = {JwtClaimTypes.Role}
            },
             new IdentityResource()
            {
                Name="profile",
                UserClaims = { JwtClaimTypes.Name,JwtClaimTypes.BirthDate,JwtClaimTypes.Gender}
            },
            new IdentityResource()
            {
                Name ="customClaims",
                UserClaims = {JwtClaimTypes.PhoneNumber,JwtClaimTypes.Email}
            }
        };
    public static IEnumerable<ApiScope> ApiScopes =>
      new List<ApiScope> {
          new ("bookapi.read","Books Read"),
          new ("bookapi.write","Manage Books"),

          new ("userapi.read","Read users"),
          new ("userapi.write","Manage Users"),
          new ("productapi.read","Read product"),
          new ("productapi.write","Manage Product")
      };
    public static IEnumerable<ApiResource> ApiResources =>
     new List<ApiResource> {
         new ("bookapi", "Book Api")
         {
             Scopes = {"bookapi.read","bookapi.write"}
         },
         new ("productapi", "Product Api")
         {
             Scopes = { "productapi.read", "productapi.write" }
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
            ClientId = "book-store-app",
            ClientName = "Book Store App",
            AllowOfflineAccess = true,
            RequirePkce = true,
            AllowedGrantTypes = GrantTypes.Code,
            ClientSecrets = { new("BookStore".Sha256())},
            AllowedScopes = {"openid","profile","roles"},
            RedirectUris = {"https://localhost:7234/signin-oidc"},
            FrontChannelLogoutUri = "https://localhost:7234/signout-oidc",
            PostLogoutRedirectUris = { "https://localhost:7234/signout-callback-oidc" },
            RequireConsent = false,
        },
          new()
        {
            ClientId = "mvc-app",
            ClientName = "Mvc  App",
            AllowOfflineAccess = true,
            RequirePkce = true,
            AllowedGrantTypes = GrantTypes.Code,
            ClientSecrets = { new("MvcApp".Sha256())},
            AllowedScopes = {"openid","profile","roles"},
            RedirectUris = {"https://localhost:7204/signin-oidc"},
            PostLogoutRedirectUris = { "https://localhost:7204/Home/Index" },
            RequireConsent = false,
        }

    };
}

