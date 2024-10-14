using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.S_02.APIs.Error;
using Store.S_02.APIs.Exentions;
using Store.S_02.Core.Dtos.Auth;
using Store.S_02.Core.Entities.Identity;
using Store.S_02.Core.Services.Contract;

namespace Store.S_02.APIs.Controllers;

public class AccountsController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountsController(IUserService userService, UserManager<AppUser> userManager, ITokenService tokenService,
        IMapper mapper)
    {
        _userService = userService;
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("login")] //* Post /api/accounts/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userService.LoginAsync(loginDto);
        if (user is null)
            return Unauthorized(new APiErrorResponse(StatusCodes.Status401Unauthorized, " invalid sign up"));
        return Ok(user);
    }

    [HttpPost("register")] //* Post /api/accounts/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = await _userService.RegisterAsync(registerDto);
        if (user is null)
            return BadRequest(new APiErrorResponse(StatusCodes.Status400BadRequest, "Email is invalid"));
        return Ok(user);
    }

    [Authorize]
    [HttpGet("GetCurrentUser")] //* Get /api/accounts/GetCurrentUser
    public async Task<IActionResult> GetCurrentUser()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null)
            return BadRequest(new APiErrorResponse(StatusCodes.Status400BadRequest, "User not found"));

        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null) return BadRequest(new APiErrorResponse(StatusCodes.Status400BadRequest, "User not found"));


        return Ok(new UserDto()
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = await _tokenService.CreateTokenAsync(user, _userManager),
        });
    }

    [Authorize]
    [HttpGet("Address")] //* Get /api/accounts/Address
    public async Task<IActionResult> GetCurrentUserAddress()
    {
        var user = await _userManager.FindByEmailWithAddressAsync(User);
        if (user is null) return BadRequest(new APiErrorResponse(StatusCodes.Status400BadRequest, "User not found"));

        return Ok(_mapper.Map<AddressDto>(user.Address));
    }
}