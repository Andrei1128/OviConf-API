using DOMAIN.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace API.Utilities;
public class ConferenceAuthorizeAttribute : ActionFilterAttribute
{
    private readonly string _roleName;
    public ConferenceAuthorizeAttribute(string roleName) => _roleName = roleName;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var claims = context.HttpContext.User.Claims;
        int conferenceId = Convert.ToInt32(context.ActionArguments["conferenceId"]);

        var result = new UnauthorizedObjectResult($"You are not {_roleName} in this conference!!");

        var adminRole = claims.SingleOrDefault(c => c.Type == ClaimTypes.Role && c.Value == IdentityData.Admin);
        if (adminRole is null)
        {
            context.Result = result;
            return;
        }

        var helperRole = claims.SingleOrDefault(c => c.Type == ClaimTypes.Role && c.Value == IdentityData.Helper);
        if (helperRole is null)
        {
            context.Result = result;
            return;
        }

        var confIds = claims.Single(c => c.Type == _roleName).Value.Split(",").ToList();
        if (!confIds.Contains(conferenceId.ToString()))
        {
            context.Result = result;
            return;
        }
    }
}
