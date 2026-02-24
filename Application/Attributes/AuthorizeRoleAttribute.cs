using Microsoft.AspNetCore.Authorization;
using Portly.Domain.ValueObjects;
using System.Linq;

namespace Portly.Application.Attributes;

public class AuthorizeRoleAttribute : AuthorizeAttribute
{
    public AuthorizeRoleAttribute(params object[] roles)
    {
        var roleStrings = roles.Select(r => r.ToString()).ToArray();
        Roles = string.Join(",", roleStrings);
    }
}
