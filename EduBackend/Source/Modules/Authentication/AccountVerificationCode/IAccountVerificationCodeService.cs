namespace EduBackend.Source.Modules.Authentication.AccountVerificationCode;

public interface IAccountVerificationCodeService
{
  Task<Model.Entity.AccountVerificationCode> UpsertAccountVerificationCode(string code, long userId);

  Task MarkAccountVerificationCodeAsConfirmed(long id);
  Task<Model.Entity.AccountVerificationCode> GetAccountVerificationCodeByUserId(long userId);
}