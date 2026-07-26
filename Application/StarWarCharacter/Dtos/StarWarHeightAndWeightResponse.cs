namespace Application.StarWarCharacter.Dtos;

public record StarWarHeightAndWeightResponse
{
    public double AverageHeight { set; get; }
    public double AverageWeight { set; get; }

    public double Percentile95Height { set; get; }
    public double Percentile95Weight { set; get; }
}