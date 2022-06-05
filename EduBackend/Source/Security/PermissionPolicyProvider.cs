using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace EduBackend.Source.Security;

internal class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
  private DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

  public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
  {
    FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
  }

  public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

  public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
  {
    if (policyName.StartsWith("permission", StringComparison.OrdinalIgnoreCase))
    {
      var policy = new AuthorizationPolicyBuilder();
      policy.AddRequirements(new PermissionRequirement(policyName));
      return Task.FromResult<AuthorizationPolicy?>(policy.Build());
    }

    return FallbackPolicyProvider.GetPolicyAsync(policyName);
  }

  public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy?>(null);
}