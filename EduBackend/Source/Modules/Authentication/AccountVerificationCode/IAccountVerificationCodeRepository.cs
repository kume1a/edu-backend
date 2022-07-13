namespace EduBackend.Source.Modules.Authentication.AccountVerificationCode;

public interface IAccountVerificationCodeRepository
{
  Task<Model.Entity.AccountVerificationCode> CreateEntity(string code, bool isVerified, long userId);
  Task<Model.Entity.AccountVerificationCode?> UpdateByUserId(long userId, string? code = null, bool? isVerified = null);
  Task<Model.Entity.AccountVerificationCode?> UpdateById(long id, string? code = null, bool? isVerified = null);
  Task<Model.Entity.AccountVerificationCode?> GetByUserId(long userId);
  Task<bool> ExistsByUserId(long userId);
}