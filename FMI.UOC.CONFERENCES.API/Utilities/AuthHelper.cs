using DOMAIN.Utilities;
using System.Security.Claims;

namespace API.Utilities;

public class AuthHelper
{
    public static bool IsConferenceAuthorized(IEnumerable<Claim> claims, int conferenceId, string claimName)
    {
        var adminRole = claims.SingleOrDefault(c => c.Type == ClaimTypes.Role && c.Value == IdentityData.Admin);
        if (adminRole is not null)
            return true;

        var helperRole = claims.SingleOrDefault(c => c.Type == ClaimTypes.Role && c.Value == IdentityData.Helper);
        if (helperRole is not null)
            return true;

        var confIds = claims.Single(c => c.Type == claimName).Value.Split(",").ToList();
        return confIds.Contains(conferenceId.ToString());
    }
}
