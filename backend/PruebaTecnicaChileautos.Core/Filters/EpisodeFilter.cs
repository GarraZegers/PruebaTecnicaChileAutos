using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaChileautos.Core.Filters
{
    public class EpisodeFilter
    {
        public string? Name { get; set; }
        public string? Episode { get; set; }


        public string ToQueryString()
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(Name))
                queryParams.Add($"name={Uri.EscapeDataString(Name)}");

            if (!string.IsNullOrWhiteSpace(Episode))
                queryParams.Add($"episode={Uri.EscapeDataString(Episode)}");

            return string.Join("&", queryParams);
        }
    }
}
