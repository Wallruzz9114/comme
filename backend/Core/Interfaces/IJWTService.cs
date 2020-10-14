using Models;

namespace Core.Interfaces
{
    public interface IJWTService
    {
        string CreateToken(AppUser appUser);
    }
}