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
using PruebaTecnicaChileautos.Core.Filters;

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

        #region CHARACTERS

        /// <summary>
        /// Entrega todos los personajes de la serie Rick and Morty
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<CharacterDto>> GetAllCharactersAsync(int page = 1)
        {
            try
            {
                string query = $"{_settings.BaseUrl}/character?page={page}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<CharacterDto>>(query);

                return response ?? new ApiResponse<CharacterDto>
                {
                    Info = new PageInfo(),
                    Results = []
                };
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener todos el personaje");
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de la obtencion de todos los personajes");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los personajes");
            }

            return new ApiResponse<CharacterDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        /// <summary>
        /// Obtiene un personaje de la serie Rick and Morty por su id
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CharacterDto>> GetSingleCharacterAsync(int characterId)
        {
            try
            {
                string query = $"{_settings.BaseUrl}/character/{characterId}";
                var response = await _httpClient.GetFromJsonAsync<CharacterDto>(query);


                if (response is null)
                {
                    return new ApiResponse<CharacterDto>
                    {
                        Info = new PageInfo(),
                        Results = []
                    };
                }
                else
                {
                    return new ApiResponse<CharacterDto>
                    {
                        Info = new PageInfo { Count = 1, Next = null, Pages = 1, Prev = null },
                        Results = [response]
                    };
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener el personar {characterId}.", characterId);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta del personaje {characterId}.", characterId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el personaje {characterId}.", characterId);
            }

            return new ApiResponse<CharacterDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

       /// <summary>
       /// Retorna una lista de personajes segun los ids de los personajes entregados
       /// </summary>
       /// <param name="characterIds"></param>
       /// <returns></returns>
        public async Task<ApiResponse<CharacterDto>> GetMultipleCharactersAsync(List<string> characterIds)
        {
            try
            {
                string query = $"{_settings.BaseUrl}/character/{string.Join(",", characterIds)}";

                if (characterIds.Count == 1)
                {
                    var single = await _httpClient.GetFromJsonAsync<CharacterDto>(query);
                    return new ApiResponse<CharacterDto>
                    {
                        Info = new PageInfo { Count = 1, Pages = 1, Next = null, Prev = null },
                        Results = single is null ? [] : [single]
                    };
                }
                else
                {
                    var list = await _httpClient.GetFromJsonAsync<List<CharacterDto>>(query);
                    return new ApiResponse<CharacterDto>
                    {
                        Info = new PageInfo { Count = list?.Count ?? 0, Pages = 1, Next = null, Prev = null },
                        Results = list ?? []
                    };
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener personajes {charactersIds}.", string.Join(",", characterIds));
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de los personajes {characterIds}.", string.Join(",", characterIds));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los personajes {characterIds}.", string.Join(",", characterIds));
            }

            return new ApiResponse<CharacterDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        /// <summary>
        /// Enterga una lista de personajes segun los filtros entregados
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CharacterDto>> GetFilteredCharacters(CharacterFilter filters)
        {
            try
            {
                string query = filters.ToQueryString();
                string url = $"{_settings.BaseUrl}/character";

                if (!string.IsNullOrWhiteSpace(query))
                {
                    url += $"?{query}";
                }

                var response = await _httpClient.GetFromJsonAsync<ApiResponse<CharacterDto>>(url);

                return response ?? new ApiResponse<CharacterDto>
                {
                    Info = new PageInfo(),
                    Results = []
                };
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener los personajes con los filtros {filters}.", filters);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de los personajes para los filtros {filters}.", filters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los personajes para los filtros {filters}.", filters);
            }

            return new ApiResponse<CharacterDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        #endregion



        #region LOCATIONS
        /// <summary>
        /// Obtiene todos los lugares de y sus descripciones completas
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<LocationDto>> GetAllLocationsAsync(int page = 1)
        {
            try
            {
                string query = $"{_settings.BaseUrl}/location?page={page}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<LocationDto>>(query);

                return response ?? new ApiResponse<LocationDto>
                {
                    Info = new PageInfo(),
                    Results = []
                };
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener todos los lugares");
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de la obtencion de todos los lugares");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los lugares");
            }

            return new ApiResponse<LocationDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        /// <summary>
        /// Obtiene un lugar en particular a partir de su id y sus descripciones completas
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<LocationDto>> GetSingleLocationAsync(int locationId)
        {
            try
            {
                string query = $"{_settings.BaseUrl}/location/{locationId}";
                var response = await _httpClient.GetFromJsonAsync<LocationDto>(query);


                if (response is null)
                {
                    return new ApiResponse<LocationDto>
                    {
                        Info = new PageInfo(),
                        Results = []
                    };
                }
                else
                {
                    return new ApiResponse<LocationDto>
                    {
                        Info = new PageInfo { Count = 1, Next = null, Pages = 1, Prev = null },
                        Results = [response]
                    };
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener el lugar {locationId}.", locationId);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta del lugar {locationId}.", locationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener el lugar {locationId}.", locationId);
            }

            return new ApiResponse<LocationDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

       /// <summary>
       /// Obtiene multiples lugares segun los ids entregados
       /// </summary>
       /// <param name="locationIds"></param>
       /// <returns></returns>
        public async Task<ApiResponse<LocationDto>> GetMultipleLocationsAsync(List<string> locationIds)
        {
            try
            {
                string query = $"{_settings.BaseUrl}/episode/{string.Join(",", locationIds)}";

                if (locationIds.Count == 1)
                {
                    var single = await _httpClient.GetFromJsonAsync<LocationDto>(query);
                    return new ApiResponse<LocationDto>
                    {
                        Info = new PageInfo { Count = 1, Pages = 1, Next = null, Prev = null },
                        Results = single is null ? [] : [single]
                    };
                }
                else
                {
                    var list = await _httpClient.GetFromJsonAsync<List<LocationDto>>(query);
                    return new ApiResponse<LocationDto>
                    {
                        Info = new PageInfo { Count = list?.Count ?? 0, Pages = 1, Next = null, Prev = null },
                        Results = list ?? []
                    };
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener lugares {locationIds}.", string.Join(",", locationIds));
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de los lugares {locationIds}.", string.Join(",", locationIds));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los lugares {locationIds}.", string.Join(",", locationIds));
            }

            return new ApiResponse<LocationDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

       /// <summary>
       /// Entrega una lista de lugares segun los filtros entregados; name, type, dimension.
       /// </summary>
       /// <param name="filters"></param>
       /// <returns></returns>
        public async Task<ApiResponse<LocationDto>> GetFilteredLocations(LocationFilter filters)
        {
            try
            {
                string query = filters.ToQueryString();
                string url = $"{_settings.BaseUrl}/location";

                if (!string.IsNullOrWhiteSpace(query))
                {
                    url += $"?{query}";
                }

                var response = await _httpClient.GetFromJsonAsync<ApiResponse<LocationDto>>(url);

                return response ?? new ApiResponse<LocationDto>
                {
                    Info = new PageInfo(),
                    Results = []
                };
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error de la red al obtener los lugares con los filtros {filters}.", filters);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error al deserializar la respuesta de los lugares para los filtros {filters}.", filters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los lugares para los filtros {filters}.", filters);
            }

            return new ApiResponse<LocationDto>
            {
                Info = new PageInfo(),
                Results = []
            };
        }

        #endregion


        #region EPISODES

        /// <summary>
        /// Metodo que obtiene todos los episodios de la serie de Rick And Morty, 
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<EpisodeDto>> GetAllEpisodesAsync(int page = 1)
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
