namespace EduBackend.Source.Modules.Permission;

public interface IPermissionService
{
  public Task<IEnumerable<string>> GetPermissionNamesByUserId(long userId);
}