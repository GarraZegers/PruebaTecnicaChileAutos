#region USING
using PruebaTecnicaChileautos.Core.Classes;
using PruebaTecnicaChileautos.Core.DTOs;
using PruebaTecnicaChileautos.Core.Filters;
#endregion


namespace PruebaTecnicaChileautos.Core.Interfaces
{
    public interface IRickAndMortyApiClient
    {
        #region CHARACTER
        Task<ApiResponse<CharacterDto>> GetAllCharactersAsync();
        Task<ApiResponse<CharacterDto>> GetSingleCharacterAsync(int characterId);
        Task<ApiResponse<CharacterDto>> GetMultipleCharactersAsync(List<string> characterIds);
        Task<ApiResponse<CharacterDto>> GetFilteredCharacters(CharacterFilter filters);
        #endregion       

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
