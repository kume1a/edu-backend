namespace EduBackend.Source.Modules.Permission;

public interface IPermissionRepository
{
  public Task<IEnumerable<string>> GetClaimValuesByUserId(long userId);
}