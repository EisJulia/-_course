namespace Lab2;

public class Contender
{
    public string Name { get; }
    public int PlaceInRate { get; set; }

    public Contender(string name, int placeInRate)
    {
        Name = name;
        PlaceInRate = placeInRate;
    }
}