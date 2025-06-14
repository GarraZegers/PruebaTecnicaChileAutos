using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaChileautos.Core.Filters
{
    public class CharacterFilter
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? Species { get; set; }
        public string? Type { get; set; }
        public string? Gender { get; set; }


        public enum ValidStatus { 
            alive,
            dead,
            unknow
        }

        public string ToQueryString() {

            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(Name))
                queryParams.Add($"name={Uri.EscapeDataString(Name)}");

            if (!string.IsNullOrWhiteSpace(Status) && Enum.TryParse<ValidStatus>(Status, out _))
                queryParams.Add($"status={Uri.EscapeDataString(Status)}");
            
            if (!string.IsNullOrWhiteSpace(Species))
                queryParams.Add($"species={Uri.EscapeDataString(Species)}");

            if (!string.IsNullOrWhiteSpace(Type))
                queryParams.Add($"type={Uri.EscapeDataString(Type)}");

            if (!string.IsNullOrWhiteSpace(Gender))
                queryParams.Add($"gender={Uri.EscapeDataString(Gender)}");

            return string.Join(",", queryParams);
        }
    }
}
