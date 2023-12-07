using AutoMapper;
using BookAPI.Data.Entities;
using BookAPI.DTOs;
using BookAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;


namespace BookAPI.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _authService = authService;
            _userManager = userManager;
            _mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var registerResult = await _authService.Register(model);

            if (registerResult.IsFailure)
            {
                return BadRequest(ResponseDto<object>.Failure(registerResult.Errors));
            }

          
            return Ok(ResponseDto<object>.Success(registerResult.Data));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {

            var loginResponse = await _authService.Login(model);
            if (loginResponse.IsFailure)
                return BadRequest(loginResponse.Errors);

            var result = ResponseDto<object>.Success(loginResponse.Data);
            return Ok(result);
        }


        [HttpGet("get-all-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _authService.GetAllRoles();

            if (result == null)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 400,
                    Error = "No roles found",
                    Message = "Error"
                });
            }

            return Ok(new ResponseDto<object>
            {
                Code = 200,
                Data = result,
                Error = "",
                Message = "OK",
                IsSuccessful = true
            });
        }
    }
}
