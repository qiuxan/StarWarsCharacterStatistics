using Application.StartWarCharacter;
using System.Net.Http.Json;
using Application.StartWarCharacter.Dtos;


namespace Infrastructure.Repositories;

public class SartWarCharacterRepository : ISartWarCharacterRepository
{
    public async Task<List<HttpCharacter>> GetData()
    {
        var result = new List<HttpCharacter>();

        // Fetching http data here https://swapi.py4e.com/api/people?format=json
        HttpClient client = new HttpClient();
        string? url = "https://swapi.py4e.com/api/people?format=json";

        while (IsValidUrl(url))
        {
            HttpCharacterResponse? response = await client.GetFromJsonAsync<HttpCharacterResponse>(url);

            List<HttpCharacter> characterList = response?.Results;

            if (characterList != null) result.AddRange(characterList);
            url = response?.Next;
        }
        
        return result;
    }

    private static bool IsValidUrl(string? value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out var uri)
               && (uri.Scheme == Uri.UriSchemeHttp
                   || uri.Scheme == Uri.UriSchemeHttps);
    }
}