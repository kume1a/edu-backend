using EduBackend.Source.Common;
using EduBackend.Source.Config;
using EduBackend.Source.Middleware;
using EduBackend.Source.Model;
using EduBackend.Source.Model.Mapper;
using EduBackend.Source.Modules.Authentication;
using EduBackend.Source.Modules.Documents;
using EduBackend.Source.Modules.Feedback;
using EduBackend.Source.Modules.Genre;
using EduBackend.Source.Modules.Permission;
using EduBackend.Source.Modules.Role;
using EduBackend.Source.Modules.User;
using EduBackend.Source.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.SwaggerUI;

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
  .AddCors()
  .AddSwaggerModule()
  .AddScoped<ExceptionMiddleware>()
  .AddDbContext<DataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql"))
  )
  .AddCommonModule()
  .AddMapperModule()
  .AddSecurityModule()
  .AddAuthenticationModule(builder.Configuration)
  .AddPermissionModule()
  .AddUserModule()
  .AddRoleModule()
  .AddDocumentModule()
  .AddGenreModule()
  .AddFeedbackModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(
    options =>
    {
      options.DocExpansion(DocExpansion.None);
      options.EnablePersistAuthorization();
    }
  );
}

app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Assets")),
  RequestPath = "/Assets"
});

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(
  corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials()
);
// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();