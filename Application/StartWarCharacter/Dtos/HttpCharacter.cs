namespace Application.StartWarCharacter.Dtos;

public record HttpCharacter()
{
    public string height { set; get; }
    public string mass { set; get; }
};

public record HttpCharacterResponse()
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
    public List<HttpCharacter> Results { get; set; } = [];
}