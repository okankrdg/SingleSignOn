using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Okan.IdentityServer.Extensions;
using Okan.IdentityServer.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//var clientService = builder.Services.BuildServiceProvider().GetRequiredService<IClientService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "oidc"; //OpenIdConnect
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
      options =>
      {
          options.ExpireTimeSpan = TimeSpan.FromHours(24);
      })
      .AddOpenIdConnect("oidc", options =>
      {
          options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          options.ClientId = "mvc-app";
          options.ClientSecret = "MvcApp";
          options.Authority = "https://localhost:7267/";
          options.ResponseType = "code";
          options.SaveTokens = true;
          options.SignedOutCallbackPath = "/Home/Index";
          options.SignedOutRedirectUri = "/";
          options.Scope.Clear();
          options.Scope.Add("openid");
          options.Scope.Add("profile");
          options.Scope.Add("roles");
          options.GetClaimsFromUserInfoEndpoint = true;
          options.ClaimActions.MapJsonKey("role", "role", "role");
          options.ClaimActions.MapUniqueJsonKey("gender", "gender", "gender");
          options.TokenValidationParameters.RoleClaimType = "role";
          options.TokenValidationParameters.NameClaimType = JwtClaimTypes.Name;

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
                  identity.AddClaim(new Claim("customClaim", "customValue"));//custom claim
              },
          };

      });
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
