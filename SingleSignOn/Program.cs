using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using SingleSignOn;
using SingleSignOn.Services;

var builder = WebApplication.CreateBuilder(args);
var conStr = builder.Configuration.GetConnectionString("SqlServer");
var assembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(conStr));
builder.Services.AddIdentityServer()
    .AddConfigurationStore(
        options => options.ConfigureDbContext =
        config => config.UseSqlServer(conStr, opt => opt.MigrationsAssembly(assembly))
    )
    .AddOperationalStore(
        options => options.ConfigureDbContext =
        config => config.UseSqlServer(conStr, opt => opt.MigrationsAssembly(assembly))
    )
    .AddInMemoryApiResources(IdentityConfig.ApiResources)
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddProfileService<IdentityProfileService>()
    .AddDeveloperSigningCredential();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.ConfigureApplicationCookie(options =>
{
        options.Cookie.Name = "IdentityServer.Cookie";
        options.LoginPath = "Account/Login";
        options.LogoutPath = "Account/Logout";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseIdentityServer();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = HttpOnlyPolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.Lax,
    Secure = CookieSecurePolicy.Always
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
