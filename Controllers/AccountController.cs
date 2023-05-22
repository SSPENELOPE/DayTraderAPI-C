using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DayTraderProAPI.Core.Entities.Identity;
using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Extensions;
using DayTraderProAPI.Dtos;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DayTraderProAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using DayTraderProAPI.Errors;
using DayTraderProAPI.Erros;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace DayTraderProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ICoinBaseSignIn _coinBaseSignIn;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper 
            mapper, ICoinBaseSignIn coinBaseSignIn)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _coinBaseSignIn = coinBaseSignIn;
        }

        [Authorize]
        [HttpGet(nameof(GetCurrentUser))]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimPrincipal(HttpContext.User);

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string Email)
        {
            return await _userManager.FindByEmailAsync(Email) != null;
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new APIResponce(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new APIResponce(401));
            return new UserDto
            {
                AppUserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<UserDto>> Register(RegistrationDto regsiterDto)
        {
            if (CheckEmailExistAsync(regsiterDto.Email).Result.Value)
            {
                return BadRequest(new APIValidationErrorResponce
                {
                    Errors = new
                    [] { "Emial is already in use" }
                });
            }
            var user = new AppUser
            {
                UserName = regsiterDto.UserName,
                Email = regsiterDto.Email,
                FirstName = regsiterDto.FirstName,
                LastName = regsiterDto.LastName,
            };
            var result = await _userManager.CreateAsync(user, regsiterDto.Password);
            if (!result.Succeeded) return BadRequest(new APIResponce(400));
            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpGet(nameof(ConnectAccountAsync))]
        public async Task<ActionResult> ConnectAccountAsync(UserDto userDto)
        {
           var tempcode = await _coinBaseSignIn.RequestTemporaryCode();
            return Ok(tempcode);
        }
    }
}
