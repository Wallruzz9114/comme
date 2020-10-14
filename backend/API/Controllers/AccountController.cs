using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using Core.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTService jwtService, IMapper mapper)
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserViewModel>> Login(LoginRequestViewModel loginRequestViewModel)
        {
            var appUser = await _userManager.FindByEmailAsync(loginRequestViewModel.Email);

            if (appUser == null) return Unauthorized(new APIResponse(StatusCodes.Status401Unauthorized));

            var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, loginRequestViewModel.Password, false);

            if (!signInResult.Succeeded) return Unauthorized(new APIResponse(StatusCodes.Status401Unauthorized));

            return new AppUserViewModel
            {
                FullName = appUser.FullName,
                Email = appUser.Email,
                Token = _jwtService.CreateToken(appUser)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUserViewModel>> Register(RegisterRequestViewModel registerRequestViewModel)
        {
            if (UserExists(registerRequestViewModel.Email).Result.Value)
                return new BadRequestObjectResult(
                    new APIValidationErrorResponse { Errors = new[] { "Email already in use" } }
                );

            var appUser = new AppUser
            {
                FullName = registerRequestViewModel.FullName,
                Email = registerRequestViewModel.Email,
                UserName = registerRequestViewModel.Email
            };

            var identityResult = await _userManager.CreateAsync(appUser, registerRequestViewModel.Password);

            if (!identityResult.Succeeded) return BadRequest(new APIResponse(StatusCodes.Status400BadRequest));

            return new AppUserViewModel
            {
                FullName = appUser.FullName,
                Email = appUser.Email,
                Token = _jwtService.CreateToken(appUser)
            };
        }

        [Authorize]
        [HttpGet("get")]
        public async Task<ActionResult<AppUserViewModel>> GetCurrentAppUser()
        {
            var appUser = await _userManager.FindByClaimsPrincipal(HttpContext.User);

            return new AppUserViewModel
            {
                Email = appUser.Email,
                Token = _jwtService.CreateToken(appUser),
                FullName = appUser.FullName
            };
        }

        [HttpGet("exists")]
        public async Task<ActionResult<bool>> UserExists([FromQuery] string email)
        {
            var result = await _userManager.FindByEmailAsync(email) != null;
            return result;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressViewModel>> GetUserAddress()
        {
            var appUser = await _userManager.FindUserByCaimPrincipalWithAddressAsync(HttpContext.User);
            var address = _mapper.Map<Address, AddressViewModel>(appUser.Address);
            return address;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<AddressViewModel>> UpdateUserAddressAsync(AddressViewModel addressViewModel)
        {
            var appUser = await _userManager.FindUserByCaimPrincipalWithAddressAsync(HttpContext.User);
            appUser.Address = _mapper.Map<AddressViewModel, Address>(addressViewModel);

            var identityResult = await _userManager.UpdateAsync(appUser);

            if (identityResult.Succeeded) return Ok(_mapper.Map<Address, AddressViewModel>(appUser.Address));

            return BadRequest("Problem updating the user's address");
        }
    }
}