using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LosExpress.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ExampleUserDTO
    {
        [JsonPropertyName("id")] 
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
