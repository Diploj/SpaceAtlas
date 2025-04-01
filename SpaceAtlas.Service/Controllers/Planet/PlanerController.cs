using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceAtlas.BL.Planet;
using SpaceAtlas.BL.Planet.Entities;
using SpaceAtlas.BL.Star.Entities;
using SpaceAtlas.Controllers.Planet.Entities;
using SpaceAtlas.Controllers.Star.Entities;
using SpaceAtlas.Validators.Planet;

namespace SpaceAtlas.Controllers.Planet;

[ApiController]
[Route("[controller]")]
public class PlanetController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<PlanetController>  _logger;
    private readonly IPlanetService _planetService;

    public PlanetController(IMapper mapper, ILogger<PlanetController> logger, IPlanetService planetService)
    {
        _mapper = mapper;
        _logger = logger;
        _planetService = planetService;
    }

    [HttpPost("create")]
    public IActionResult CreatePlanet([FromBody] PlanetCreateRequest request)
    {
        var validationResult = new PlanetCreateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            try
            {
                var createPlanetModel = _mapper.Map<PlanetModel>(request);
                var planetId = _planetService.Create(createPlanetModel);
                return Ok(planetId);
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
    public IActionResult UpdatePlanet([FromBody] PlanetUpdateRequest request)
    {
        var validationResult = new PlanetUpdateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var updatePlanetModel = _mapper.Map<PlanetModel>(request);
            try
            {
                var starId = _planetService.Update(updatePlanetModel);
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
    public IActionResult GetAllPlanet()
    {
        try
        {
            var planets = _planetService.GetAll();
            var response = _mapper.Map<IList<PlanetResponse>>(planets);
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
    public IActionResult GetFilteredPlanet([FromQuery] PlanetFilter filter)
    {
        try
        {
            var planetFilterModel = _mapper.Map<FilterPlanetModel>(filter);
            var planets = _planetService.GetAll(planetFilterModel);
            var response = _mapper.Map<IList<PlanetResponse>>(planets);
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
    public IActionResult GetPlanetById([FromQuery] Guid id)
    {
        try
        {
            var planetModel = _planetService.GetById(id);
            return Ok(_mapper.Map<PlanetResponse>(planetModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest("ERROR");
        }
    }
}