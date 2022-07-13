using EduBackend.Source.Model;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Authentication.AccountVerificationCode;

public class AccountVerificationCodeRepository : IAccountVerificationCodeRepository
{
  private readonly DataContext _db;

  public AccountVerificationCodeRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<Model.Entity.AccountVerificationCode> CreateEntity(string code, bool isVerified, long userId)
  {
    var entity = new Model.Entity.AccountVerificationCode
    {
      Code = code,
      IsVerified = isVerified,
      UserId = userId
    };

    await _db.AccountVerificationCodes.AddAsync(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task<Model.Entity.AccountVerificationCode?> UpdateByUserId(
    long userId,
    string? code = null,
    bool? isVerified = null)
  {
    var entity = await _db.AccountVerificationCodes
      .SingleOrDefaultAsync(accountVerificationCode => accountVerificationCode.UserId == userId);

    if (entity is null)
    {
      return null;
    }

    return await UpdateEntity(entity, code, isVerified);
  }

  public async Task<Model.Entity.AccountVerificationCode?> UpdateById(long id, string? code, bool? isVerified)
  {
    var entity = await _db.AccountVerificationCodes
      .SingleOrDefaultAsync(accountVerificationCode => accountVerificationCode.Id == id);

    if (entity is null)
    {
      return null;
    }

    return await UpdateEntity(entity, code, isVerified);
  }

  public async Task<Model.Entity.AccountVerificationCode?> GetByUserId(long userId)
  {
    return await _db.AccountVerificationCodes
      .AsNoTracking()
      .SingleOrDefaultAsync(accountVerificationCode => accountVerificationCode.UserId == userId);
  }

  public async Task<bool> ExistsByUserId(long userId)
  {
    return await _db.AccountVerificationCodes
      .AnyAsync(accountVerificationCode => accountVerificationCode.UserId == userId);
  }
  
  private async Task<Model.Entity.AccountVerificationCode> UpdateEntity(
    Model.Entity.AccountVerificationCode entity,
    string? code,
    bool? isVerified)
  {
    if (code is not null) entity.Code = code;
    if (isVerified is not null) entity.IsVerified = isVerified.Value;

    await _db.SaveChangesAsync();

    return entity;
  }
}