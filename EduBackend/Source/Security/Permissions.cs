using EduBackend.Source.Common;

namespace EduBackend.Source.Security;

public static class AppAction
{
  public const string Create = nameof(Create);
  public const string Read = nameof(Read);
  public const string Update = nameof(Update);
  public const string Delete = nameof(Delete);
}

public static class AppResource
{
  public const string Roles = nameof(Roles);
  public const string Permissions = nameof(Permissions);
}

public static class AppPermissions
{
  public static readonly AppPermission ReadPermission =
    new("Read permission", AppAction.Read, AppResource.Permissions);

  public static readonly AppPermission ReadRole =
    new("Read role", AppAction.Read, AppResource.Roles);

  public static readonly AppPermission CreateRole =
    new("Create role", AppAction.Create, AppResource.Roles);

  public static readonly AppPermission UpdateRole =
    new("Update role", AppAction.Update, AppResource.Roles);

  public static readonly AppPermission DeleteRole =
    new("Delete role", AppAction.Delete, AppResource.Roles);
  
  public static readonly AppPermission[] All = {
    ReadPermission,
    ReadRole,
    CreateRole,
    UpdateRole,
    DeleteRole,
  };
}

public record AppPermission(string Description, string Action, string Resource)
{
  public string Name => NameFor(Action, Resource);

  public static string NameFor
    (string action, string resource) =>
    $"{AppClaimTypes.Permission}.{resource}.{action}";
}