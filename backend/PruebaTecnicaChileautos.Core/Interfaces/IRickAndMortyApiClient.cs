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
        Task<ApiResponse<EpisodeDto>> GetEpisodesAsync(int page);
    }
}
