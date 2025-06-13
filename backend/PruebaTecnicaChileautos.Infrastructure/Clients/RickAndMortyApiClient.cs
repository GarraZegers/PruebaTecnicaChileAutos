using PruebaTecnicaChileautos.Core.Classes;
using PruebaTecnicaChileautos.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PruebaTecnicaChileautos.Core.Interfaces;
using PruebaTecnicaChileautos.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace PruebaTecnicaChileautos.Infrastructure.Clients
{
    public class RickAndMortyApiClient : IRickAndMortyApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RickAndMortyApiClient> _logger;
        private readonly RickAndMortyOptions _settings;

        public RickAndMortyApiClient(HttpClient httpClient, ILogger<RickAndMortyApiClient> logger, IOptions<RickAndMortyOptions> settings)
        {
            _httpClient = httpClient;
            _logger = logger;
            _settings= settings.Value;
        }
        /// <summary>
        /// Metodo que ontiene episodios de la serie de Rick And Morty, recibiendo el numero de 
        /// pagina como parámetro de entrada.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ApiResponse<EpisodeDto>> GetEpisodesAsync(int page)
        {   
            try
            {
                string query = $"{_settings.BaseUrl}/episode?page={page}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<EpisodeDto>>(query);

                return response ?? new ApiResponse<EpisodeDto>
                {
                    Info = new PageInfo(),
                    Results = []
                };
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener episodios para la página {Page}.", page);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de episodios para la página {Page}.", page);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener episodios para la página {Page}.", page);
            }

            return new ApiResponse<EpisodeDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }
    }
}
