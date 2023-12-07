using BookAPI.Data;
using BookAPI.Data.Entities;
using BookAPI.Data.Entities.Enums;
using BookAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly BookDbContext _context;
        RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGeneratorService _jwtTokenGenerator;
        public AuthService(
            IConfiguration config, 
            SignInManager<AppUser> signInManager, 
            UserManager<AppUser> userManager, 
            BookDbContext context,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGeneratorService jwtTokenGeneratorService)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGeneratorService;
        }


        public async Task<Result<UserDto>> Register(RegistrationRequestDto registrationRequestDto)
        {
            AppUser user = new()
            {
                FirstName = registrationRequestDto.FirstName,
                LastName = registrationRequestDto.LastName,
                Email = registrationRequestDto.Email,
                UserName = registrationRequestDto.UserName,
            };

            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (!result.Succeeded)
                return Result.Failure<UserDto>(result.Errors.Select(e => new Error(e.Code, e.Description)));

            var userToReturn = _context.AppUsers.First(u => u.UserName == registrationRequestDto.Email);

            await AssignRole(userToReturn.Email, UserRolesConstants.Regular);
            var roles = await _userManager.GetRolesAsync(userToReturn);

            UserDto appUserDTO = new()
            {
                Id = userToReturn.Id,
                Email = userToReturn.Email,
                FirstName = userToReturn.FirstName,
                LastName = userToReturn.LastName,
                PhoneNumber = userToReturn.PhoneNumber,
                RoleName = roles
                
            };

            return Result.Success(appUserDTO);
        }


        public async Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            var user = _context.AppUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Email.ToLower());
            if (user is null)
                return Result.Failure<LoginResponseDto>(new[]
                    { new Error("Auth.Error", "username or password not correct") });

            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!isValid)
                return Result.Failure<LoginResponseDto>(new[]
                    { new Error("Auth.Error", "username or password not correct") });

            //If user is found, generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            var appUserDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                RoleName = roles
            };

            var loginResponseDto = new LoginResponseDto()
            {
                User = appUserDto,
                Token = token
            };

            return Result.Success(loginResponseDto);
        }


        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _context.AppUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user == null) return false;

            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                //create role if it does not exist
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }

            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            if (roles == null) throw new Exception("You have no roles created yet");

            return roles;
        }

        
    }
}
