using EduBackend.Source.Model;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Authentication.RecoverPasswordRequest;

public class RecoverPasswordRequestRepository : IRecoverPasswordRequestRepository
{
  private readonly DataContext _db;

  public RecoverPasswordRequestRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<Model.Entity.RecoverPasswordRequest> CreateEntity(
    string email,
    string code,
    bool isVerified,
    string? uuid)
  {
    var entity = new Model.Entity.RecoverPasswordRequest
    {
      Email = email,
      Code = code,
      IsVerified = isVerified,
      Uuid = uuid
    };

    await _db.AddAsync(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task DeleteAllByEmail(string email)
  {
    var requests =
      await _db.RecoverPasswordRequests.Where(request => request.Email == email).ToListAsync();
    _db.RecoverPasswordRequests.RemoveRange(requests);

    await _db.SaveChangesAsync();
  }

  public Task<Model.Entity.RecoverPasswordRequest?> GetByEmail(string email)
  {
    return _db.RecoverPasswordRequests.SingleOrDefaultAsync(request => request.Email == email);
  }

  public async Task<Model.Entity.RecoverPasswordRequest?> UpdateById(
    long id,
    string? email = null,
    string? code = null,
    bool? isVerified = null,
    string? uuid = null)
  {
    var entity =
      await _db.RecoverPasswordRequests.SingleOrDefaultAsync(request => request.Id == id);
    if (entity is null)
    {
      return null;
    }

    if (email is not null) entity.Email = email;
    if (code is not null) entity.Code = code;
    if (isVerified is not null) entity.IsVerified = isVerified.Value;
    if (uuid is not null) entity.Uuid = uuid;

    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task<Model.Entity.RecoverPasswordRequest?> GetByUuid(string uuid)
  {
    return await _db.RecoverPasswordRequests.SingleOrDefaultAsync(request => request.Uuid == uuid);
  }

  public async Task DeleteEntity(Model.Entity.RecoverPasswordRequest recoverPasswordRequest)
  {
    _db.RecoverPasswordRequests.Remove(recoverPasswordRequest);
    await _db.SaveChangesAsync();
  }
}