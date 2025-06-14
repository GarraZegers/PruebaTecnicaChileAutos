using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaChileautos.Core.Filters;
using PruebaTecnicaChileautos.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaTecnicaChileautos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {

        private readonly IRickAndMortyApiClient _episodeService;
        private readonly ILogger<EpisodesController> _logger;

        public EpisodesController(IRickAndMortyApiClient apiService, ILogger<EpisodesController> logger)
        {
            _episodeService = apiService;
            _logger = logger;
            
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredEpisodes([FromQuery] EpisodeFilter query) {
            try
            {
                var result = await _episodeService.GetFilteredEpisodes(query);

                if (result == null || result.Results == null || result.Results.Count == 0) {
                
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
                _logger.LogError(ex, "Error inesperado al obtener episodios con el filtro query de valor {query}", query);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("multiple")]
        public async Task<IActionResult> GetMultipleEpisodes([FromQuery] string episodes)
        {
            try
            {
                var episodesIds = episodes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => id.Trim())
                    .Where(id => int.TryParse(id, out _))
                    .ToList();

                if (episodesIds.Count == 0) return BadRequest("Numeros de episodios invalidos");

                var result = await _episodeService.GetMultipleEpisodesAsync(episodesIds);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron resultados para la página solicitada.");
                }

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                _logger.LogWarning(arg, "Solicitud inválida para el parámetro episodes de valor {episodes}", episodes);
                return BadRequest("Parámetros inválidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener episodios con el filtro episodes de valor {episodes}", episodes);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetSingleEpisodes([FromQuery] int episode)
        {
            try
            {
                var result = await _episodeService.GetSingleEpisodeAsync(episode);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron resultados para el episodio solicitado.");
                }

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                _logger.LogWarning(arg, "Solicitud inválida para el parámetro episode de valor {episode}", episode);
                return BadRequest("Parámetros inválidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el episodio de valor {episode}", episode);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEpisodes(int page = 1)
        {
            try
            {
                var result = await _episodeService.GetAllEpisodesAsync(page);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron episodios.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los episodios");
                return StatusCode(500, "Error inesperado.");
            }
        }

    }
}
