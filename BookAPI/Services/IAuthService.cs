using BookAPI.Data.Entities;
using BookAPI.DTOs;
using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace BookAPI.Services
{
    public interface IAuthService
    {

        Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
        //string GenerateJWT(AppUser user, IList<string> roles, IList<Claim> userclaim);
        Task<Result<UserDto>> Register(RegistrationRequestDto registrationRequestDto);
        Task<bool> AssignRole(string email, string roleId);
        Task<List<IdentityRole>> GetAllRoles();

    }
}
