using System.Security.Claims;

namespace EduBackend.Source.Common;

public static class ClaimsPrincipalX
{
  public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
  {
    var claim = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == AppClaimTypes.UserId);
    if (claim is null)
    {
      throw new System.Exception("Could not find claim " + AppClaimTypes.UserId);
    }

    if (!long.TryParse(claim.Value,out var userId))
    {
      throw new System.Exception("Claim type " + AppClaimTypes.UserId + " is not type of long");
    }

    return userId;
  }
}