using EduBackend.Source.Model.Common;

namespace EduBackend.Source.Modules.Role;

public interface IRoleRepository
{
  Task<bool> ExistsByName(string name);
  Task<bool> ExistsOtherByName(long roleId, string name);
  Task<Model.Entity.Role> CreateEntity(string name, string description);
  Task<Model.Entity.Role?> UpdateById(long id, string? name, string? description);
  Task<Model.Entity.Role?> GetById(long id);
  Task<DataPage<Model.Entity.Role>> Filter(string? searchQuery, int page, int pageSize);
}