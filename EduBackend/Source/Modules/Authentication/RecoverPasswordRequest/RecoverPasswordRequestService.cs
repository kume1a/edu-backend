using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;

namespace EduBackend.Source.Modules.Authentication.RecoverPasswordRequest;

public class RecoverPasswordRequestService : IRecoverPasswordRequestService
{
  private readonly IRecoverPasswordRequestRepository _recoverPasswordRequestRepository;
  private readonly IConfiguration _config;

  public RecoverPasswordRequestService(
    IRecoverPasswordRequestRepository recoverPasswordRequestRepository,
    IConfiguration config)
  {
    _recoverPasswordRequestRepository = recoverPasswordRequestRepository;
    _config = config;
  }

  public async Task<Model.Entity.RecoverPasswordRequest> CreateRecoverPasswordRequest(
    string email,
    string code,
    bool isVerified,
    string? uuid = null)
  {
    await _recoverPasswordRequestRepository.DeleteAllByEmail(email);

    return await _recoverPasswordRequestRepository.CreateEntity(email, code, isVerified, uuid);
  }

  public async Task<Model.Entity.RecoverPasswordRequest> GetRecoverPasswordRequestByEmail(
    string email)
  {
    var request = await _recoverPasswordRequestRepository.GetByEmail(email);
    if (request is null)
    {
      throw new NotFoundException(ExceptionMessageCode.RecoverPasswordRequestNotFound);
    }

    return request;
  }

  public Task ValidateRecoverPasswordExpiration(
    Model.Entity.RecoverPasswordRequest recoverPasswordRequest)
  {
    int.TryParse(
      _config["Authentication:RecoverPasswordRequestTimeoutInMinutes"],
      out var timeoutInMinutes
    );

    if (DateTime.UtcNow.Subtract(recoverPasswordRequest.CreatedAt).TotalMinutes > timeoutInMinutes)
    {
      throw new BadRequestException(ExceptionMessageCode.RecoverPasswordRequestTimedOut);
    }

    return Task.CompletedTask;
  }

  public async Task MarkRecoverPasswordRequestAsVerified(long recoverPasswordRequestId, string uuid)
  {
    var request = await _recoverPasswordRequestRepository.UpdateById(
      recoverPasswordRequestId,
      uuid: uuid,
      isVerified: true
    );
    
    if (request is null)
    {
      throw new NotFoundException(ExceptionMessageCode.RecoverPasswordRequestNotFound);
    }
  }

  public async Task<Model.Entity.RecoverPasswordRequest> GetRecoverPasswordRequestByUuid(string uuid)
  {
    var request = await _recoverPasswordRequestRepository.GetByUuid(uuid);
    if (request is null)
    {
      throw new NotFoundException(ExceptionMessageCode.RecoverPasswordRequestNotFound);
    }

    return request;
  }

  public async Task DeleteRecoverPasswordRequest(Model.Entity.RecoverPasswordRequest recoverPasswordRequest)
  {
    await _recoverPasswordRequestRepository.DeleteEntity(recoverPasswordRequest);
  }
}