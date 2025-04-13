using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpaceAtlas.Algoritms;
using SpaceAtlas.BL.User;
using SpaceAtlas.BL.User.Entities;
using SpaceAtlas.Controllers.User.Entities;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.Controllers.User;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthController> _logger ;
    private readonly TokenGenerator _tokenGenerator;

    public AuthController(IUserService userService, IMapper mapper,
        ILogger<AuthController> logger, TokenGenerator tokenGenerator)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateRequest request)
    {
        var user = _mapper.Map<UserModel>(request);
        var result = await _userService.Create(user, request.Password);
        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully" });
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userService.GetByName(request.UserName);
        if (user != null && await _userService.CheckPassword(user, request.Password))
        {
            var roles = await _userService.GetRole(user);
            var token = _tokenGenerator.Generate(user, roles);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return BadRequest("Invalid username or password" );
        //return Unauthorized();
    }
}