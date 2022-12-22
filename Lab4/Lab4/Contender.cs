namespace Lab4;

public class Contender
{
    public string? Name { get; set; }
    public int PlaceInRate { get; set; }

    public Contender(string? name, int placeInRate)
    {
        Name = name;
        PlaceInRate = placeInRate;
    }

    public static List<Contender> MapToContenderList(string?[]? names, int[] rates)
    {
        var list = new List<Contender>();
        for (var i = 0; i < rates.Length; i++)
        {
            if (names == null)
            {
                list.Add(new Contender(null, rates[i]));
                
            }
            else
            {
                list.Add(new Contender(names[i], rates[i]));
            }
        }

        return list;
    }

    public static void DivideList(List<Contender> contenders, ref string?[] names, ref int[] rates)
    {
        for (var i = 0; i < names.Length; i++)
        {
            names[i] = contenders[i].Name;
            rates[i] = contenders[i].PlaceInRate;
        }
    }
}