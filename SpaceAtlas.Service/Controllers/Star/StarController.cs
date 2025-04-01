using AutoMapper;
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

    [HttpPost("create")]
    public IActionResult CreateStar([FromBody] StarCreateRequest request)
    {
        var validationResult = new StarCreateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            try
            {
                var createStarModel = _mapper.Map<StarModel>(request);
                var starId = _starService.Create(createStarModel);
                return Ok(starId);
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
    public IActionResult UpdateStar([FromBody] StarUpdateRequest request)
    {
        var validationResult = new StarUpdateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var updateStarModel = _mapper.Map<StarModel>(request);
            try
            {
                var starId = _starService.Update(updateStarModel);
                return Ok(starId);
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
    public IActionResult GetAllStars()
    {
        try
        {
            var stars = _starService.GetAll();
            var response = _mapper.Map<IList<StarResponse>>(stars);
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
    public IActionResult GetFilteredStars([FromQuery] StarFilter filter)
    {
        try
        {
            var starFilterModel = _mapper.Map<FilterStarModel>(filter);
            var stars = _starService.GetAll(starFilterModel);
            var response = _mapper.Map<IList<StarResponse>>(stars);
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
    public IActionResult GetStarById([FromQuery] Guid id)
    {
        try
        {
            var starModel = _starService.GetById(id);
            return Ok(_mapper.Map<StarResponse>(starModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest("ERROR");
        }
    }
}