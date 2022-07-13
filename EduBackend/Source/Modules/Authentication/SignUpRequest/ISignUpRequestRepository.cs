namespace EduBackend.Source.Modules.Authentication.SignUpRequest;

public interface ISignUpRequestRepository
{
  Task<Model.Entity.SignUpRequest> CreateEntity(
    string email,
    string code,
    bool isVerified,
    string? uuid);

  Task<Model.Entity.SignUpRequest?> UpdateById(
    long id,
    string? email = null,
    string? code = null,
    bool? isVerified = null,
    string? uuid = null
  );

  Task<Model.Entity.SignUpRequest?> GetByUuid(string uuid);

  Task<Model.Entity.SignUpRequest?> GetByEmail(string email);
  Task DeleteEntity(Model.Entity.SignUpRequest signUpRequest);
  Task DeleteAllByEmail(string email);
}