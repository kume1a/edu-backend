namespace EduBackend.Source.Model.DTO.Authentication;

public class AuthenticationPayloadDto
{
  public string AccessToken { get; set; }

  public string RefreshToken { get; set; }
}