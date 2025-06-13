using Microsoft.AspNetCore.Mvc;
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

        public EpisodesController(IRickAndMortyApiClient episodeService, ILogger<EpisodesController> logger) {
            _episodeService = episodeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEpisodes([FromQuery] int page = 1) {
            try
            {
                var result = await _episodeService.GetEpisodesAsync(page);

                if (result == null || result.Results == null || result.Results.Count == 0) {
                
                    return NotFound("No se encontraron resultados para la página solicitada.");
                }

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                _logger.LogWarning(arg, "Solicitud inválida para el parámetro page de valor {page}", page);
                return BadRequest("Parámetros inválidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener episodios con el parámetro page de valor {page}", page);
                return StatusCode(500, "Error inesperado.");
            }
        }
    }
}
