namespace Application.StarWarCharacter;

public interface IStarWarCharacterRepository
{
    public Task<List<Domain.Entities.StarWarCharacter>> GetData();
}