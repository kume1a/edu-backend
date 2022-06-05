using EduBackend.Source.Config;
using EduBackend.Source.Middleware;
using EduBackend.Source.Model;
using EduBackend.Source.Modules.Authentication;
using EduBackend.Source.Modules.Permission;
using EduBackend.Source.Modules.User;
using EduBackend.Source.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
  options =>
  {
    var policy = new AuthorizationPolicyBuilder()
      .RequireAuthenticatedUser()
      .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
  }
);


builder.Services
  .AddSwaggerModule()
  .AddScoped<ExceptionMiddleware>()
  .AddDbContext<DataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql"))
  )
  .AddAuthenticationModule(builder.Configuration)
  .AddPermissionModule()
  .AddUserModule()
  .AddSecurityModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options => options.EnablePersistAuthorization());
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();