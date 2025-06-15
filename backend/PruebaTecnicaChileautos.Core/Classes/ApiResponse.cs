using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PruebaTecnicaChileautos.Core.Classes
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("info")]
        public PageInfo Info { get; set; } = new();

        [JsonPropertyName("results")]
        public List<T> Results { get; set; } = new();
    }
}
