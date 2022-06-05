namespace EduBackend.Source.Modules.Permission;

public interface IPermissionRepository
{
  public Task<IEnumerable<string>> GetNamesByUserId(long userId);
}