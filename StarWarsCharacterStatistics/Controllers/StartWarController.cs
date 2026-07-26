using Application.StartWarCharacter;
using Microsoft.AspNetCore.Mvc;

namespace StarWarsCharacterStatistics.Controllers;

[ApiController]
public class StartWarController : ControllerBase
{
    private readonly CalculateHeightWeightDataUseCase _calculateHeightWeightDataUseCase;

    public StartWarController(CalculateHeightWeightDataUseCase calculateHeightWeightDataUseCase)
    {
        _calculateHeightWeightDataUseCase = calculateHeightWeightDataUseCase;
    }
    
    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GetData()
    {
        var result =await _calculateHeightWeightDataUseCase.Execute();
        double averageHeight = result.AverageHeight;
        double averageWeight = result.AverageWeight;

        double percentile95Height = result.Percentile95Height;
        double percentile95Weight = result.Percentile95Weight;

        return Ok($@"
        Average Height: {averageHeight} cm
        Average Weight: {averageWeight} kg
        95th Percentile Height: {percentile95Height} cm 
        95th Percentile Weight: {percentile95Weight} kg");
    }
}