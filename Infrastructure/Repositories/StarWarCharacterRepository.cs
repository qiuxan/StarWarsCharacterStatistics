using Application.StarWarCharacter;
using System.Net.Http.Json;
using Domain.Entities;
using Infrastructure.Dtos;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Repositories;

public class StarWarCharacterRepository : IStarWarCharacterRepository
{
    private readonly HttpClient _client;
    private readonly ILogger<StarWarCharacterRepository> _logger;

    public StarWarCharacterRepository(
        HttpClient client, 
        ILogger<StarWarCharacterRepository> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<List<StarWarCharacter>> GetData()
    {
        var characters = new List<HttpCharacter>();

        // Fetching http data here https://swapi.py4e.com/api/people?format=json
        string? url = "https://swapi.py4e.com/api/people?format=json";
        while (IsValidUrl(url))
        {
            _logger.LogInformation("Fetching Star Wars characters from {Url}", url);
            HttpCharacterResponse? response = await _client.GetFromJsonAsync<HttpCharacterResponse>(url);

            List<HttpCharacter>? characterList = response?.Results;
            if (characterList != null)
            {
                characters.AddRange(characterList);
                _logger.LogInformation("Fetched {Count} characters from current page", characterList.Count);
            }

            url = response?.Next;
        }

        if (characters.Count == 0) return new List<StarWarCharacter>();

        var validCharacters = characters
            .Where(x => IsStringOfNumber(x.Mass) && IsStringOfNumber(x.Height))
            .Select(x => new StarWarCharacter { Height = double.Parse(x.Height), Weight = double.Parse(x.Mass) })
            .ToList();
        _logger.LogInformation("Fetched {Count} valid Star Wars characters", validCharacters.Count);

        return validCharacters;
    }


    private static bool IsValidUrl(string? value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out var uri)
               && (uri.Scheme == Uri.UriSchemeHttp
                   || uri.Scheme == Uri.UriSchemeHttps);
    }

    private static bool IsStringOfNumber(string weight) => double.TryParse(weight, out _);
}