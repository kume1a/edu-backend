namespace EduBackend.Source.Model.Entity;

public class AccountVerificationCode
{
  public long Id { get; set; }
  public Boolean IsVerified { get; set; }
  
  public string Code { get; set; }
  
  public long UserId { get; set; }
  
  public User User { get; set; }
}