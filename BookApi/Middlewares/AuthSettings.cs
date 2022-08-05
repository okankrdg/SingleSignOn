using BookApi.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq;

namespace BookApi.Middlewares;

public static class AuthSettings
{
    public static void AddMvcAuthentication(this AuthenticationBuilder appBuilder,IAuthSettings authSettings)
    {
        var client = authSettings.GetClientDetail();
        var urls = authSettings.GetClientUrl();
        var customClaims = authSettings.GetCustomClaims();
        appBuilder.AddOpenIdConnect("oidc", options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.ClientId = client.ClientId;
            options.ClientSecret = client.ClientSecret;
            options.Authority = urls.Authority; //"https://localhost:7267/";
            options.ResponseType = "code";
            options.SaveTokens = true;
            options.SignedOutCallbackPath = urls.LogOutAddress;
            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("roles");
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClaimActions.MapJsonKey("role", "role", "role");
            options.ClaimActions.MapUniqueJsonKey("gender", "gender", "gender");
            options.TokenValidationParameters.RoleClaimType = "role";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationScheme
            };
            options.Events = new OpenIdConnectEvents
            {
                OnTokenValidated = async context =>
                {
                    var identity = context.Principal?.Identity as ClaimsIdentity;
                    if (identity == null)
                    {
                        return;
                    }
                    identity.AddClaims(customClaims);
                }
            };

        });
    }
    public static void AddApiAuthentacation(this AuthenticationBuilder authenticationBuilder,IAuthSettings authSettings)
    {
        var client = authSettings.GetClientDetail();
        var urls = authSettings.GetClientUrl();
        var customClaims = authSettings.GetCustomClaims();
        authenticationBuilder.AddJwtBearer(options =>
        {
            options.Authority = urls.Authority;
        });
    }
}
