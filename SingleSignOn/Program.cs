using Microsoft.EntityFrameworkCore;
using SingleSignOn;

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
    ).AddDeveloperSigningCredential();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseIdentityServer();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
