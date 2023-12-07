using BookAPI.Data.Entities;

namespace BookAPI.Services
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(AppUser appUser, IEnumerable<string> roles);
    }
}
