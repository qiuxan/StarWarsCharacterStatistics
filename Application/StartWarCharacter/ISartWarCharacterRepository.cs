using Application.StartWarCharacter.Dtos;

namespace Application.StartWarCharacter;

public interface ISartWarCharacterRepository
{
    public Task<List<HttpCharacter>>  GetData();
}