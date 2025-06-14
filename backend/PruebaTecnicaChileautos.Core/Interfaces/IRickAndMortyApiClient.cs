using PruebaTecnicaChileautos.Core.Classes;
using PruebaTecnicaChileautos.Core.DTOs;
using PruebaTecnicaChileautos.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaChileautos.Core.Interfaces
{
    public interface IRickAndMortyApiClient
    {
        #region LOCATION
        Task<ApiResponse<LocationDto>> GetAllLocationsAsync();
        Task<ApiResponse<LocationDto>> GetSingleLocationAsync(int locationId);
        Task<ApiResponse<LocationDto>> GetMultipleLocationsAsync(List<string> locationIds);
        Task<ApiResponse<LocationDto>> GetFilteredLocations(LocationFilter filters);
        #endregion

        #region EPISODES
        Task<ApiResponse<EpisodeDto>> GetAllEpisodesAsync();
        Task<ApiResponse<EpisodeDto>> GetSingleEpisodeAsync(int episode);
        Task<ApiResponse<EpisodeDto>> GetMultipleEpisodesAsync(List<string> episodes);
        Task<ApiResponse<EpisodeDto>> GetFilteredEpisodes(EpisodeFilter filters);
        #endregion
    }
}
