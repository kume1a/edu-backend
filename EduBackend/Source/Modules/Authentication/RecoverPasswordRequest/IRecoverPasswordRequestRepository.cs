namespace EduBackend.Source.Modules.Authentication.RecoverPasswordRequest;

public interface IRecoverPasswordRequestRepository
{
  Task<Model.Entity.RecoverPasswordRequest> CreateEntity(
    string email,
    string code,
    bool isVerified,
    string? uuid);

  Task DeleteAllByEmail(string email);
  Task<Model.Entity.RecoverPasswordRequest?> GetByEmail(string email);

  Task<Model.Entity.RecoverPasswordRequest?> UpdateById(
    long id,
    string? email = null,
    string? code = null,
    bool? isVerified = null,
    string? uuid = null);

  Task<Model.Entity.RecoverPasswordRequest?> GetByUuid(string uuid);
  Task DeleteEntity(Model.Entity.RecoverPasswordRequest recoverPasswordRequest);
}