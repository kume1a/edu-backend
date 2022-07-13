namespace EduBackend.Source.Model.Entity;

public class SignUpRequest
{
  public long Id { get; set; }

  public string Email { get; set; }

  public string Code { get; set; }

  public bool IsVerified { get; set; }

  public string? Uuid { get; set; }
}