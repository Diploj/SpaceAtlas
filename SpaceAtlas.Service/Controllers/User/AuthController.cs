using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpaceAtlas.Algoritms;
using SpaceAtlas.Controllers.User.Entities;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.Controllers.User;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly ILogger<AuthController> _logger ;

    public AuthController(UserManager<UserEntity> userManager, ILogger<AuthController> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateRequest request)
    {
        var user = new UserEntity { UserName = request.Username, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user, "User");
        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully" });
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = TokenGenerator.Generate(user, roles);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }
}