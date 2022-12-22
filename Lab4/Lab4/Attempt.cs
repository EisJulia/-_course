using System.ComponentModel.DataAnnotations;

namespace Lab4;

public class Attempt
{
    public int Id { get; set; }
    public int Happiness { get; set; }
    public int ChosenNumber { get; set; }
    public string?[]? ContendersNames { get; set; }
    public int[] ContendersRates { get; set; }
}