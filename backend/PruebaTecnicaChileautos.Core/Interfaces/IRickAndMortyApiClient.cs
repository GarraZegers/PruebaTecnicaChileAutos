using PruebaTecnicaChileautos.Core.Classes;
using PruebaTecnicaChileautos.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaChileautos.Core.Interfaces
{
    public interface IRickAndMortyApiClient
    {
        Task<ApiResponse<EpisodeDto>> GetAllEpisodesAsync();
        Task<ApiResponse<EpisodeDto>> GetSingleEpisodeAsync(int episode);
        Task<ApiResponse<EpisodeDto>> GetMultipleEpisodesAsync(List<string> episodes);
        Task<ApiResponse<EpisodeDto>> GetFilteredEpisodes(EpisodeFilter filters);
    }
}
