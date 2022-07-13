namespace EduBackend.Source.Modules.Authentication.SignUpRequest;

public interface ISignUpRequestService
{
  Task<Model.Entity.SignUpRequest> CreateSignUpRequest(string email, string code);
  Task<Model.Entity.SignUpRequest> GetSignUpRequestByUuid(string uuid);
  Task<Model.Entity.SignUpRequest> GetSignUpRequestByEmail(string email);
  Task MarkSignUpRequestAsVerified(long signUpRequestId, string uuid);
  Task DeleteSignUpRequest(Model.Entity.SignUpRequest signUpRequest);
}