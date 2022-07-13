using EduBackend.Source.Model;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Authentication.SignUpRequest;

public class SignUpRequestRepository : ISignUpRequestRepository
{
  private readonly DataContext _db;

  public SignUpRequestRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<Model.Entity.SignUpRequest> CreateEntity(
    string email,
    string code,
    bool isVerified,
    string? uuid)
  {
    var signUpRequest = new Model.Entity.SignUpRequest
    {
      Email = email,
      Code = code,
      IsVerified = isVerified,
      Uuid = uuid
    };

    _db.SignUpRequests.Add(signUpRequest);
    await _db.SaveChangesAsync();

    return signUpRequest;
  }

  public async Task<Model.Entity.SignUpRequest?> UpdateById(
    long id,
    string? email = null,
    string? code = null,
    bool? isVerified = null,
    string? uuid = null)
  {
    var signUpRequest = await _db.SignUpRequests.SingleOrDefaultAsync(request => request.Id == id);
    if (signUpRequest is null)
    {
      return null;
    }

    if (email is not null) signUpRequest.Email = email;
    if (code is not null) signUpRequest.Code = code;
    if (isVerified is not null) signUpRequest.IsVerified = isVerified.Value;
    if (uuid is not null) signUpRequest.Uuid = uuid;

    await _db.SaveChangesAsync();

    return signUpRequest;
  }

  public async Task<Model.Entity.SignUpRequest?> GetByUuid(string uuid)
  {
    return await _db.SignUpRequests.SingleOrDefaultAsync(
      signUpRequest => signUpRequest.Uuid == uuid
    );
  }

  public async Task<Model.Entity.SignUpRequest?> GetByEmail(string email)
  {
    return await _db.SignUpRequests.SingleOrDefaultAsync(
      signUpRequest => signUpRequest.Email == email
    );
  }

  public async Task DeleteEntity(Model.Entity.SignUpRequest signUpRequest)
  {
    _db.SignUpRequests.Remove(signUpRequest);
    await _db.SaveChangesAsync();
  }

  public async Task DeleteAllByEmail(string email)
  {
    var signUpRequests =
      await _db.SignUpRequests.Where(request => request.Email == email).ToListAsync();
    _db.SignUpRequests.RemoveRange(signUpRequests);
    await _db.SaveChangesAsync();
  }
}