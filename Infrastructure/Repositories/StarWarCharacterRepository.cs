using Application.StarWarCharacter;
using System.Net.Http.Json;
using Domain.Entities;
using Infrastructure.Dtos;


namespace Infrastructure.Repositories;

public class StarWarCharacterRepository : IStarWarCharacterRepository
{
    private readonly HttpClient _client;

    public StarWarCharacterRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<StarWarCharacter>> GetData()
    {
        var characters = new List<HttpCharacter>();

        // Fetching http data here https://swapi.py4e.com/api/people?format=json
        string? url = "https://swapi.py4e.com/api/people?format=json";

        while (IsValidUrl(url))
        {
            HttpCharacterResponse? response = await _client.GetFromJsonAsync<HttpCharacterResponse>(url);

            List<HttpCharacter> characterList = response?.Results;

            if (characterList != null) characters.AddRange(characterList);
            url = response?.Next;
        }

        if (characters.Count == 0) return new List<StarWarCharacter>();


        return
            characters
                .Where(x => IsStringOfNumber(x.Mass) && IsStringOfNumber(x.Height))
                .Select(x => new StarWarCharacter { Height = double.Parse(x.Height), Weight = double.Parse(x.Mass) })
                .ToList();
    }


    private static bool IsValidUrl(string? value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out var uri)
               && (uri.Scheme == Uri.UriSchemeHttp
                   || uri.Scheme == Uri.UriSchemeHttps);
    }

    private static bool IsStringOfNumber(string weight) => double.TryParse(weight, out _);
}