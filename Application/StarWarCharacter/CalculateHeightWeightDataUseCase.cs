using Application.StarWarCharacter.Dtos;

namespace Application.StarWarCharacter;

public class CalculateHeightWeightDataUseCase
{
    private readonly IStarWarCharacterRepository _starWarCharacterRepository;

    public CalculateHeightWeightDataUseCase(IStarWarCharacterRepository starWarCharacterRepository)
    {
        _starWarCharacterRepository = starWarCharacterRepository;
    }
    
    private double Calculate95Percentile(List<double> list)
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("List is empty.");
        }

        var sortedList = list.OrderBy(x => x).ToList();

        int index = (int)Math.Ceiling(sortedList.Count * 0.95) - 1;

        return sortedList[index];
    }

    public async Task<StarWarHeightAndWeightResponse> Execute()
    {
        var data = await _starWarCharacterRepository.GetData();

        var heightWithNumberGroup =
            data
                .Select(x => x.Height).ToList();

        var weightWithNumberGroup =
            data
                .Select(x => x.Weight).ToList();

        return new StarWarHeightAndWeightResponse
        {
            AverageHeight = Math.Round(heightWithNumberGroup.Average(), 2),
            AverageWeight = Math.Round(weightWithNumberGroup.Average(), 2),
            Percentile95Height = Calculate95Percentile(heightWithNumberGroup),
            Percentile95Weight = Calculate95Percentile(weightWithNumberGroup),
        };
    }
}