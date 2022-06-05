using System.ComponentModel.DataAnnotations.Schema;

namespace EduBackend.Source.Model.Entity;

public class Permission
{
  public long Id { get; set; }

  public string Name { get; set; }

  public string? Description { get; set; }
  
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime CreatedAt{ get; set; }

  public ICollection<RolePermission> RolePermissions { get; set; }
}