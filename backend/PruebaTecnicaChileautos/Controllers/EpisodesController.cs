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

        public EpisodesController(IRickAndMortyApiClient episodeService) {
            _episodeService = episodeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEpisodes([FromQuery] int page = 1) {
            var result = await _episodeService.GetEpisodesAsync(page);
            return Ok(result);
        }
    }
}
