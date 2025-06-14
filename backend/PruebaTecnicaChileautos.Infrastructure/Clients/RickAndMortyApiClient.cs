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

        #region EPISODES

        /// <summary>
        /// Metodo que obtiene todos los episodios de la serie de Rick And Morty, 
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<EpisodeDto>> GetAllEpisodesAsync()
        {
            try
            {
                string query = $"{_settings.BaseUrl}/episode";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<EpisodeDto>>(query);

                return response ?? new ApiResponse<EpisodeDto>
                {
                    Info = new PageInfo(),
                    Results = []
                };
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener todos los episodios");
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de la obtencion de todos los episodios");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los episodios");
            }

            return new ApiResponse<EpisodeDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        /// <summary>
        /// Metodo que obtiene un episodio de la serie de Rick And Morty, 
        /// recibiendo el numero de episodio como parámetro de entrada.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ApiResponse<EpisodeDto>> GetSingleEpisodeAsync(int episode)
        {   
            try
            {
                string query = $"{_settings.BaseUrl}/episode/{episode}";
                var response = await _httpClient.GetFromJsonAsync<EpisodeDto>(query);


                if (response is null) {
                    return new ApiResponse<EpisodeDto>
                    {
                        Info = new PageInfo(),
                        Results = []
                    };
                }
                else {
                    return new ApiResponse<EpisodeDto>
                    {
                        Info = new PageInfo { Count = 1, Next = null, Pages = 1, Prev = null },
                        Results = [response]
                    };
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener el episodio {episode}.", episode);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta del episodio {episode}.", episode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el episodio {episode}.", episode);
            }

            return new ApiResponse<EpisodeDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        /// <summary>
        /// Método que obtiene multiples episodios de la serie Rick And Morty
        /// </summary>
        /// <param name="episodes"></param>
        /// <returns></returns>
        public async Task<ApiResponse<EpisodeDto>> GetMultipleEpisodesAsync(List<string> episodes)
        {
            try
            {
                string query = $"{_settings.BaseUrl}/episode/{string.Join(",", episodes)}";

                if (episodes.Count == 1)
                {
                    var single = await _httpClient.GetFromJsonAsync<EpisodeDto>(query);
                    return new ApiResponse<EpisodeDto>
                    {
                        Info = new PageInfo { Count = 1, Pages = 1, Next = null, Prev = null },
                        Results = single is null ? [] : [single]
                    };
                }
                else {
                    var list = await _httpClient.GetFromJsonAsync<List<EpisodeDto>>(query);
                    return new ApiResponse<EpisodeDto>
                    {
                        Info = new PageInfo { Count = list?.Count ?? 0, Pages = 1, Next = null, Prev = null },
                        Results = list ?? []
                    };
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener los episodios {episodios}.", string.Join(",", episodes));
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de los episodios {episodios}.", string.Join(",", episodes));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los episodios {episodios}.", string.Join(",", episodes));
            }

            return new ApiResponse<EpisodeDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        /// <summary>
        /// Entrega una lista de episodios segun los filtros validos ingresados.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<ApiResponse<EpisodeDto>> GetFilteredEpisodes(EpisodeFilter filters)
        {
            try
            {
                string query = filters.ToQueryString();
                string url = $"{_settings.BaseUrl}/episode";

                if (!string.IsNullOrWhiteSpace(query))
                {
                    url += $"?{query}";
                }

                var response = await _httpClient.GetFromJsonAsync<ApiResponse<EpisodeDto>>(url);

                return response ?? new ApiResponse<EpisodeDto>
                {
                    Info = new PageInfo(),
                    Results = []
                };
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener episodios con los filtros {filters}.", filters);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de episodios para los filtros {filters}.", filters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener episodios para los filtros {filters}.", filters);
            }

            return new ApiResponse<EpisodeDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        #endregion
    }
}
