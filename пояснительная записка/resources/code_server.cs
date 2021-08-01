using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
namespace AltaiRPlatformApi
{
public class Program
{
public static void Main(string[] args)
{
CreateHostBuilder(args).Build().Run();
}
public static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(webBuilder =>
{
webBuilder.UseStartup<Startup>();
});
}
}
Sturtup.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AltaiR.Configuration;
using Microsoft.EntityFrameworkCore;
using AltaiR.Data.Contexts;
using AltaiR.Unity;
using Unity;
using AltaiR.Logic.Services;
using AltaiR.Logic.Services.Impl;
using AltaiR.Logic.Tools;
using AltaiR.Logic.Tools.Impl;
namespace AltaiRPlatformApi
{
public class Startup
{
public Startup(IConfiguration configuration)
{
Configuration = configuration;
}
public IConfiguration Configuration { get; }
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
services.AddDbContext<AppDbContext>(o =>
o.UseSqlServer(Configuration.GetConnectionString("DebugConnectionStringMainDatabase"), x =>
x.MigrationsAssembly("AltaiR.Migrations")));
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
options.RequireHttpsMetadata = true;
options.TokenValidationParameters = new TokenValidationParameters
{
ValidateIssuer = true,
ValidIssuer = AuthConfig.ISSUER,
ValidateAudience = true,
ValidAudience = AuthConfig.AUDIENCE,
ValidateLifetime = false,
ValidateIssuerSigningKey = true,
IssuerSigningKey = AuthConfig.GetSymmetricSecurityKey()
};
});
services.AddSingleton<IUnityContainer>(UnityContainerFactory.Container);
services.AddScoped<IAltaiRDatabaseUnitOfWorkFactory, AltaiRDatabaseUnitOfWorkFactoryImpl>();
services.AddScoped<IAwsService, AwsServiceImpl>();
services.AddScoped<IBlogService, BlogServiceImpl>();
services.AddScoped<IPostService, PostServiceImpl>();
services.AddScoped<IUserService, UserServiceImpl>();
services.AddScoped<IAuthorInviteService, AuthorInviteServiceImpl>();
services.AddCors();
services.AddControllers();
}
// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
if (env.IsDevelopment())
{
app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
endpoints.MapControllers();
});
}
}
}
