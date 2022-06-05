namespace EduBackend.Source.Model.Entity;

public class RolePermission
{
  public long Id { get; set; }

  public Role Role { get; set; }

  public long RoleId { get; set; }

  public Permission Permission { get; set; }

  public long PermissionId { get; set; }
}