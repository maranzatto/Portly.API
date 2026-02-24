namespace Portly.Application.Interfaces.Security
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}

