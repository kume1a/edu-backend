namespace EduBackend.Source.Modules.Permission;

public interface IPermissionRepository
{
  Task<IEnumerable<string>> GetClaimValuesByUserId(long userId);
}