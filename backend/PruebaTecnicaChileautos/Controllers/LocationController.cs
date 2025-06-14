using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaChileautos.Core.Filters;
using PruebaTecnicaChileautos.Core.Interfaces;

namespace PruebaTecnicaChileautos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IRickAndMortyApiClient _apiService;
        private readonly ILogger<LocationController> _logger;

        public LocationController(IRickAndMortyApiClient apiService, ILogger<LocationController> logger)
        {
            _apiService = apiService;
            _logger = logger;

        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredLocations([FromQuery] LocationFilter query)
        {
            try
            {
                var result = await _apiService.GetFilteredLocations(query);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {

                    return NotFound("No se encontraron resultados para la página solicitada.");
                }

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                _logger.LogWarning(arg, "Solicitud inválida para el parámetro query de valor {query}", query);
                return BadRequest("Parámetros inválidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los lugares con el filtro query de valor {query}", query);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("multiple")]
        public async Task<IActionResult> GetMultipleLocations([FromQuery] string locations)
        {
            try
            {
                var locationIds = locations.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => id.Trim())
                    .Where(id => int.TryParse(id, out _))
                    .ToList();

                if (locationIds.Count == 0) return BadRequest("Numeros de lugares invalidos");

                var result = await _apiService.GetMultipleLocationsAsync(locationIds);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron resultados para la página solicitada.");
                }

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                _logger.LogWarning(arg, "Solicitud inválida para el parámetro id de lugares de valor {locations}", locations);
                return BadRequest("Parámetros inválidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los lugares con el filtro di de lugares de valor {locations}", locations);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetSingleLocation([FromQuery] int locationId)
        {
            try
            {
                var result = await _apiService.GetSingleEpisodeAsync(locationId);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron resultados para el lugar solicitado.");
                }

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                _logger.LogWarning(arg, "Solicitud inválida para el parámetro locationId de valor {locationId}", locationId);
                return BadRequest("Parámetros inválidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el lugar de id {locationId}", locationId);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEpisodes()
        {
            try
            {
                var result = await _apiService.GetAllLocationsAsync();

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron lugares.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los lugares");
                return StatusCode(500, "Error inesperado.");
            }
        }
    }
}
