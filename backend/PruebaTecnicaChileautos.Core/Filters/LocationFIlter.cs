using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaChileautos.Core.Filters
{
    public class LocationFilter
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Dimension { get; set; }

        public string ToQueryString()
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(Name))
                queryParams.Add($"name={Uri.EscapeDataString(Name)}");

            if (!string.IsNullOrWhiteSpace(Type))
                queryParams.Add($"name={Uri.EscapeDataString(Type)}");

            if (!string.IsNullOrWhiteSpace(Dimension))
                queryParams.Add($"name={Uri.EscapeDataString(Dimension)}");

            return string.Join("&", queryParams);
        }
    }
}
