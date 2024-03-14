using Lorby.Application.Common.Identity;
using Lorby.Application.Common.Identity.Services;
using BC = BCrypt.Net.BCrypt;

namespace Lorby.Infrastructure.Common.Identity.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }
    
    public bool ValidatePassword(string password, string hashPassword)
    {
        return BC.Verify(password, hashPassword);
    }
}