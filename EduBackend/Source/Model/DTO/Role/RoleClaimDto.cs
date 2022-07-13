namespace EduBackend.Source.Model.DTO.Role;

public class RoleClaimDto
{
  public long Id { get; set; }

  public string? Description { get; set; }

  public DateTime CreatedAt { get; set; }

  public long RoleId { get; set; }

  public string ClaimValue { get; set; }
}