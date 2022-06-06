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

// public static class AppPermissions
// {
//     private static readonly AppPermission[] _all = {
//         new("Create Users", AppAction.Create, AppResource.Users),
//         new("Read Users", AppAction.Read, AppResource.Users),
//         new("Update Users", AppAction.Update, AppResource.Users),
//         new("Delete Users", AppAction.Delete, AppResource.Users),
//     };
//
//     public static IReadOnlyList<AppPermission> All { get; } = new ReadOnlyCollection<AppPermission>(_all);
// }

public record AppPermission(string Description, string Action, string Resource)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"{AppClaimTypes.Permission}.{resource}.{action}";
}
