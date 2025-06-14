using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaChileautos.Core.Filters;
using PruebaTecnicaChileautos.Core.Interfaces;

namespace PruebaTecnicaChileautos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IRickAndMortyApiClient _apiService;
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(IRickAndMortyApiClient apiService, ILogger<CharacterController> logger)
        {
            _apiService = apiService;
            _logger = logger;

        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredCharacters([FromQuery] CharacterFilter query)

        {
            try
            {
                var result = await _apiService.GetFilteredCharacters(query);

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
                _logger.LogError(ex, "Error inesperado al obtener los personajes con el filtro query de valor {query}", query);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("multiple")]
        public async Task<IActionResult> GetMultipleCharacters([FromQuery] string characters)
        {
            try
            {
                var characterIds = characters.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => id.Trim())
                    .Where(id => int.TryParse(id, out _))
                    .ToList();

                if (characterIds.Count == 0) return BadRequest("Numeros de personajes invalidos");

                var result = await _apiService.GetMultipleCharactersAsync(characterIds);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron resultados para la página solicitada.");
                }

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                _logger.LogWarning(arg, "Solicitud inválida para el parámetro id de personajes de valor {characters}", characters);
                return BadRequest("Parámetros inválidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los lugares con el filtro de personajes de valor {characters}", characters);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetSingleCharacter([FromQuery] int characterId)
        {
            try
            {
                var result = await _apiService.GetSingleCharacterAsync(characterId);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron resultados para el personaje solicitado.");
                }

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                _logger.LogWarning(arg, "Solicitud inválida para el parámetro characterId de valor {characterId}", characterId);
                return BadRequest("Parámetros inválidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el personaje de id {characterId}", characterId);
                return StatusCode(500, "Error inesperado.");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCharacters(int page = 1)
        {
            try
            {
                var result = await _apiService.GetAllCharactersAsync(page);

                if (result == null || result.Results == null || result.Results.Count == 0)
                {
                    return NotFound("No se encontraron personajes.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los personajes");
                return StatusCode(500, "Error inesperado.");
            }
        }
    }
}
