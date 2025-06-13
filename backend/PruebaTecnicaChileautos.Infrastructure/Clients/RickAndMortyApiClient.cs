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

        public async Task<ApiResponse<EpisodeDto>> GetEpisodesAsync(int page)
        {
            ApiResponse<EpisodeDto> response = null;
            try
            {
                string query = string.Format("{0}/episode?page={1}", _settings.BaseUrl, page);
                
                response = await _httpClient.GetFromJsonAsync<ApiResponse<EpisodeDto>>(query);
            }
            catch (Exception ex)
            {

                throw;
            }
            // Mapear a DTO, manejar errores, devolver paginación

            return response;
        }
    }
}
