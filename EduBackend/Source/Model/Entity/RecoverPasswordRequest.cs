namespace EduBackend.Source.Model.Entity;

public class RecoverPasswordRequest
{
  public long Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public string Email { get; set; }
  
  public bool IsVerified { get; set; }

  public string Code { get; set; }

  public string? Uuid { get; set; }
}