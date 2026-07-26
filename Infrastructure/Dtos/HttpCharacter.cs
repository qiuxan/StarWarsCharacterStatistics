using System.Text.Json.Serialization;

namespace Infrastructure.Dtos;

public record HttpCharacter()
{
    [JsonPropertyName("height")]
    public string Height { get; set; } = string.Empty;
    
    [JsonPropertyName("mass")]
    public string Mass { get; set; } = string.Empty;
};

public record HttpCharacterResponse()
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
    public List<HttpCharacter> Results { get; set; } = [];
}