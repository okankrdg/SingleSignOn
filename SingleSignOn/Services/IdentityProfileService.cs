using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;

namespace SingleSignOn.Services;

public class IdentityProfileService : IProfileService
{
    private readonly IUserService _userService;

    public IdentityProfileService(IUserService userService)
    {
        _userService = userService;
    }
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var userId = context.Subject.GetSubjectId();
        if (userId.IsNullOrEmpty())
        {
            return;
        }
        var user = _userService.GetById(int.Parse(userId));
        if (user == null) { return; }
        var claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.Gender,user.Gender),
                new Claim(JwtClaimTypes.PhoneNumber,"+507"),
                new Claim(JwtClaimTypes.Name,user.Name),
                new Claim(JwtClaimTypes.Email,user.Email),
                new Claim(JwtClaimTypes.BirthDate,user.BirthDate.ToString()),
                new Claim(JwtClaimTypes.Id,user.Id.ToString()),
            };
        user.Roles.ForEach(r => claims.Add(new Claim(JwtClaimTypes.Role, r)));
        var requestedClaims = claims.Where(p => context.RequestedClaimTypes.Contains(p.Type)).ToList();
        context.IssuedClaims = requestedClaims;
        context.AddRequestedClaims(requestedClaims);
    }


    public async Task IsActiveAsync(IsActiveContext context)
    {
        var data = context.IsActive;
        await Task.CompletedTask;
    }
}

