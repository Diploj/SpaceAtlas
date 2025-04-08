using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreatePlanet([FromBody] PlanetCreateRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var validationResult = new PlanetCreateValidator().Validate(request);
        if (validationResult.IsValid && userId != null)
        {
            try
            {
                var createPlanetModel = _mapper.Map<PlanetModel>(request);
                createPlanetModel.UserId = Guid.Parse(userId);
                var planetId = await _planetService.Create(createPlanetModel);
                return Ok(planetId);
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
    public async Task<IActionResult> UpdatePlanet([FromBody] PlanetUpdateRequest request)
    {
        var validationResult = new PlanetUpdateValidator().Validate(request);
        if (validationResult.IsValid)
        {
            try
            {
                var updatePlanetModel = _mapper.Map<PlanetModel>(request);
                Guid planetId;
                if(User.IsInRole("Admin"))
                    planetId = await _planetService.Update(updatePlanetModel);
                else
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    updatePlanetModel.UserId = Guid.Parse(userId);
                    planetId = await _planetService.Update(updatePlanetModel);
                }
                return Ok(planetId);
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
    public async Task<IActionResult> GetAllPlanet()
    {
        try
        {
            var planets = await _planetService.GetAll();
            var response = _mapper.Map<IList<PlanetResponse>>(planets);
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
    public async Task<IActionResult> GetFilteredPlanet([FromQuery] PlanetFilter filter)
    {
        try
        {
            var planetFilterModel = _mapper.Map<FilterPlanetModel>(filter);
            var planets = await _planetService.GetAll(planetFilterModel);
            var response = _mapper.Map<IList<PlanetResponse>>(planets);
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
    public async Task<IActionResult> GetPlanetById([FromQuery] Guid id)
    {
        try
        {
            var planetModel = await _planetService.GetById(id);
            return Ok(_mapper.Map<PlanetResponse>(planetModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeletePlanet([FromQuery] Guid id)
    {
        try
        {
            if(User.IsInRole("Admin"))
                await _planetService.Delete(id);
            else
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _planetService.Delete(id, Guid.Parse(userId));
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