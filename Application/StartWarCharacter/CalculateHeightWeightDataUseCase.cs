using System.Text.Json;
using Application.StartWarCharacter.Dtos;

namespace Application.StartWarCharacter;

public class CalculateHeightWeightDataUseCase
{
    private readonly ISartWarCharacterRepository _sartWarCharacterRepository;

    public CalculateHeightWeightDataUseCase(ISartWarCharacterRepository sartWarCharacterRepository)
    {
        _sartWarCharacterRepository = sartWarCharacterRepository;
    }

    private bool IsStingOfNumber(string value)
        => double.TryParse(value, out _);

    private double Calculate95Percentile(List<double> list)
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("List is empty.");
        }

        int index = (int)Math.Ceiling(list.Count * 0.95) - 1;

        return list[index];
    }

    public async Task<StarWarHeightAndWeightResponse> Execute()
    {
        var data = await _sartWarCharacterRepository.GetData();

        var heightWithNumberGroup =
            data
                .Where(x => IsStingOfNumber(x.height))
                .Select(x => double.Parse(x.height)).ToList();

        var weightWithNumberGroup =
            data
                .Where(x => IsStingOfNumber(x.mass))
                .Select(x => double.Parse(x.mass)).ToList();

        return new StarWarHeightAndWeightResponse
        {
            AverageHeight = Math.Round(heightWithNumberGroup.Average(), 2),
            AverageWeight = Math.Round(weightWithNumberGroup.Average(), 2),
            Percentile95Height = Calculate95Percentile(heightWithNumberGroup),
            Percentile95Weight = Calculate95Percentile(weightWithNumberGroup),
        };
    }
}