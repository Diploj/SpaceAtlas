using AutoMapper;
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

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] UserCreateRequest request)
    {
        var validationResult = new UserCreateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            try
            {
                var createUserModel = _mapper.Map<UserModel>(request);
                var userId = _userService.Create(createUserModel);
                return Ok(userId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        _logger.LogError(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpPost("update")]
    public IActionResult UpdateUser([FromBody] UserUpdateRequest request)
    {
        var validationResult = new UserUpdateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var updateUserModel = _mapper.Map<UserModel>(request);
            try
            {
                var userId = _userService.Update(updateUserModel);
                return Ok(userId);
            }
            catch (Exception e)
            {
                return BadRequest("ERROR");
            }
        }

        _logger.LogError(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }
    
    [HttpGet]
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
    }
    
    [HttpGet]
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
    }
    
    [HttpGet]
    [Route("info")]
    public IActionResult GetUserById([FromQuery] Guid id)
    {
        try
        {
            var userModel = _userService.GetById(id);
            return Ok(_mapper.Map<UserResponse>(userModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest("ERROR");
        }
    }
}