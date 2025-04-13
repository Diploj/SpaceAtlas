using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAtlas.BL.User;
using SpaceAtlas.BL.User.Entities;
using SpaceAtlas.Controllers.User.Entities;
using SpaceAtlas.Validators.User;

namespace SpaceAtlas.Controllers.User;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger ;
    private readonly IUserService _userService;

    public UserController(IMapper mapper, ILogger<UserController> logger, IUserService userService)
    {
        _mapper = mapper;
        _logger = logger;
        _userService = userService;
    }
    
    [Authorize]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
    {
        var validationResult = new UserUpdateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            
            var updateUserModel = _mapper.Map<UserModel>(request);
            try
            {   
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                updateUserModel.Id = Guid.Parse(userId);
                var result = await _userService.Update(updateUserModel, request.Password);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("ERROR");
            }
        }

        _logger.LogError(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }
    
    /*[HttpGet]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _userService.GetAll();
            var response = _mapper.Map<IList<UserResponse>>(users);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest("ERROR");
        }
    }*/
    
    /*[HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter filter)
    {
        try
        {
            var userFilterModel = _mapper.Map<FilterUserModel>(filter);
            var users = _userService.GetAll(userFilterModel);
            var response = _mapper.Map<IList<UserResponse>>(users);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest("ERROR"); 
        }
    }*/
    
    [Authorize]
    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> GetUserById([FromQuery] Guid id)
    {
        try
        {
            var userModel = await _userService.GetById(id);
            return Ok(_mapper.Map<UserResponse>(userModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _userService.Delete(Guid.Parse(userId));
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.Message);
        }
    }
}