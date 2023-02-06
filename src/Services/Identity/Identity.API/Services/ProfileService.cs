#region Corpspace© Apache-2.0
// Copyright 2023 The Corpspace Technologies
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace Corpspace.Services.Identity.API.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    async public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault()?.Value;

        var user = await _userManager.FindByIdAsync(subjectId);
        if (user == null)
            throw new ArgumentException("Invalid subject identifier");

        var claims = GetClaimsFromUser(user);
        context.IssuedClaims = claims.ToList();
    }

    async public Task IsActiveAsync(IsActiveContext context)
    {
        var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault()?.Value;
        var user = await _userManager.FindByIdAsync(subjectId);

        context.IsActive = false;

        if (user != null)
        {
            if (_userManager.SupportsUserSecurityStamp)
            {
                var securityStamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
                if (securityStamp != null)
                {
                    var dbSecurityStamp = await _userManager.GetSecurityStampAsync(user);
                    if (dbSecurityStamp != securityStamp)
                        return;
                }
            }

            context.IsActive =
                !user.LockoutEnabled ||
                !user.LockoutEnd.HasValue ||
                user.LockoutEnd <= DateTime.Now;
        }
    }

    private IEnumerable<Claim> GetClaimsFromUser(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, user.Id),
            new Claim(JwtClaimTypes.PreferredUserName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        if (!string.IsNullOrWhiteSpace(user.Name))
            claims.Add(new Claim("name", user.Name));

        if (!string.IsNullOrWhiteSpace(user.LastName))
            claims.Add(new Claim("last_name", user.LastName));

        if (!string.IsNullOrWhiteSpace(user.Country))
            claims.Add(new Claim("address_country", user.Country));

        if (_userManager.SupportsUserEmail)
        {
            claims.AddRange(new[]
            {
                new Claim(JwtClaimTypes.Email, user.Email!),
                new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
            });
        }

        if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            claims.AddRange(new[]
            {
                new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
                new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
            });
        }

        return claims;
    }
}