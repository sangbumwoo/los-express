using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LosExpress.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class PoiDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
