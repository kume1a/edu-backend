using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;

namespace EduBackend.Source.Modules.Authentication.SignUpRequest;

public class SignUpRequestService : ISignUpRequestService
{
  private readonly ISignUpRequestRepository _signUpRequestRepository;

  public SignUpRequestService(ISignUpRequestRepository signUpRequestRepository)
  {
    _signUpRequestRepository = signUpRequestRepository;
  }

  public  async Task<Model.Entity.SignUpRequest> CreateSignUpRequest(string email, string code)
  {
    await _signUpRequestRepository.DeleteAllByEmail(email);
    
    return await _signUpRequestRepository.CreateEntity(
      email,
      code: code,
      isVerified: false,
      uuid: null
    );
  }

  public async Task<Model.Entity.SignUpRequest> GetSignUpRequestByUuid(string uuid)
  {
    var signUpRequest = await _signUpRequestRepository.GetByUuid(uuid);
    if (signUpRequest is null)
    {
      throw new NotFoundException(ExceptionMessageCode.SignUpRequestNotFound);
    }

    return signUpRequest;
  }

  public async Task<Model.Entity.SignUpRequest> GetSignUpRequestByEmail(string email)
  {
    var signUpRequest = await _signUpRequestRepository.GetByEmail(email);
    if (signUpRequest is null)
    {
      throw new NotFoundException(ExceptionMessageCode.SignUpRequestNotFound);
    }

    return signUpRequest;
  }

  public async Task MarkSignUpRequestAsVerified(long signUpRequestId, string uuid)
  {
    var request = await _signUpRequestRepository.UpdateById(
      signUpRequestId,
      uuid: uuid,
      isVerified: true
    );

    if (request is null)
    {
      throw new NotFoundException(ExceptionMessageCode.SignUpRequestNotFound);
    }
  }

  public Task DeleteSignUpRequest(Model.Entity.SignUpRequest signUpRequest)
  {
    return _signUpRequestRepository.DeleteEntity(signUpRequest);
  }
}