namespace EduBackend.Source.Model.DTO.Role;

public class RoleDto
{
  public long Id { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }

  public DateTime CreatedAt { get; set; }

  public IEnumerable<RoleClaimDto> Claims { get; set; }
}