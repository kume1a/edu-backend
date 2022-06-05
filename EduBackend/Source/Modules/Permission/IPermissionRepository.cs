using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Permission;

namespace EduBackend.Source.Modules.Permission;

public interface IPermissionRepository
{
  public Task<IEnumerable<string>> GetNamesByUserId(long userId);
  public Task<DataPageDto<PermissionDto>> Filter(int page, int pageSize);
}