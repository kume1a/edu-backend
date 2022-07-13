namespace EduBackend.Source.Modules.Authentication.RecoverPasswordRequest;

public interface IRecoverPasswordRequestService
{
  Task<Model.Entity.RecoverPasswordRequest> CreateRecoverPasswordRequest(
    string email,
    string code,
    bool isVerified,
    string? uuid = null);

  Task<Model.Entity.RecoverPasswordRequest> GetRecoverPasswordRequestByEmail(string email);

  Task ValidateRecoverPasswordExpiration(
    Model.Entity.RecoverPasswordRequest recoverPasswordRequest);

  Task MarkRecoverPasswordRequestAsVerified(long recoverPasswordRequestId, string uuid);
  Task<Model.Entity.RecoverPasswordRequest> GetRecoverPasswordRequestByUuid(string uuid);
  Task DeleteRecoverPasswordRequest(Model.Entity.RecoverPasswordRequest recoverPasswordRequest);
}