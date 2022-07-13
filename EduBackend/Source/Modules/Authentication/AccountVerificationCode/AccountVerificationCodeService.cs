using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;

namespace EduBackend.Source.Modules.Authentication.AccountVerificationCode;

public class AccountVerificationCodeService : IAccountVerificationCodeService
{
  private readonly IAccountVerificationCodeRepository _accountVerificationCodeRepository;

  public AccountVerificationCodeService(IAccountVerificationCodeRepository accountVerificationCodeRepository)
  {
    _accountVerificationCodeRepository = accountVerificationCodeRepository;
  }

  public async Task<Model.Entity.AccountVerificationCode> UpsertAccountVerificationCode(string code, long userId)
  {
    if (await _accountVerificationCodeRepository.ExistsByUserId(userId))
    {
      return (await _accountVerificationCodeRepository.UpdateByUserId(userId, code: code))!;
    }

    return await _accountVerificationCodeRepository.CreateEntity(code, isVerified: false, userId);
  }

  public async Task MarkAccountVerificationCodeAsConfirmed(long id)
  {
    var accountVerificationCode = await _accountVerificationCodeRepository.UpdateById(id, isVerified: true);
    if (accountVerificationCode is null)
    {
      throw new NotFoundException(ExceptionMessageCode.AccountVerificationCodeNotFound);
    }
  }

  public async Task<Model.Entity.AccountVerificationCode> GetAccountVerificationCodeByUserId(long userId)
  {
    var accountVerificationCode = await _accountVerificationCodeRepository.GetByUserId(userId);
    if (accountVerificationCode is null)
    {
      throw new NotFoundException(ExceptionMessageCode.AccountVerificationCodeNotFound);
    }

    return accountVerificationCode;
  }
}