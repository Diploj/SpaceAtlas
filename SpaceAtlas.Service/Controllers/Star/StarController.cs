using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SpaceAtlas.BL.Star;
using SpaceAtlas.BL.Star.Entities;
using SpaceAtlas.Controllers.Star.Entities;
using SpaceAtlas.Validators.Star;

namespace SpaceAtlas.Controllers.Star;

[ApiController]
[Route("[controller]")]
public class StarController : ControllerBase
{   
    private readonly IMapper _mapper;
    private readonly ILogger<StarController> _logger;
    private readonly IStarService _starService;

    public StarController(IMapper mapper, ILogger<StarController> logger, IStarService starService)
    {
        _mapper = mapper;
        _logger = logger;
        _starService = starService;
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateStar([FromBody] StarCreateRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var validationResult = new StarCreateValidator().Validate(request);
        if (validationResult.IsValid && userId != null)
        {
            try
            {
                var createStarModel = _mapper.Map<StarModel>(request);
                createStarModel.UserId = Guid.Parse(userId);
                var starId = await _starService.Create(createStarModel);
                return Ok(starId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
        }
        _logger.LogError(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [Authorize]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateStar([FromBody] StarUpdateRequest request)
    {
        var validationResult = new StarUpdateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            try
            {
                var updateStarModel = _mapper.Map<StarModel>(request);
                Guid starId;
                if(User.IsInRole("Admin"))
                    starId = await _starService.Update(updateStarModel);
                else
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    updateStarModel.UserId = Guid.Parse(userId);
                    starId = await _starService.Update(updateStarModel);
                }
                return Ok(starId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
        }

        _logger.LogError(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllStars()
    {
        try
        {
            var stars = await _starService.GetAll();
            var response = _mapper.Map<IList<StarResponse>>(stars);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetFilteredStars([FromQuery] StarFilter filter)
    {
        try
        {
            var starFilterModel = _mapper.Map<FilterStarModel>(filter);
            var stars = await _starService.GetAll(starFilterModel);
            var response = _mapper.Map<IList<StarResponse>>(stars);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.Message); 
        }
    }
    
    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> GetStarById([FromQuery] Guid id)
    {
        try
        {
            var starModel = await _starService.GetById(id);
            return Ok(_mapper.Map<StarResponse>(starModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteStar([FromQuery] Guid id)
    {
        try
        {
            if (User.IsInRole("Admin"))
                await _starService.Delete(id);
            else
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
                await _starService.Delete(id, Guid.Parse(userId));
            }
            return Ok("Запись успешно удалена");
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.Message);
        }
    }
}