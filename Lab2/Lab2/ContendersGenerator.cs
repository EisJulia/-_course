namespace Lab2;

public class ContendersGenerator
{
    private readonly string _nameFilePath;
    private List<Contender>? _contenders;
    private List<string>? _contendersNames;
    private const int ContendersNum = 100;

    public ContendersGenerator(string path)
    {
        _nameFilePath = path;
    }

    public ContendersGenerator()
    {
        _nameFilePath = "D:\\Study\\Lab2\\Lab2\\names.txt";
    }
    public List<Contender> GenerateContenders()
    {
        _contenders = new List<Contender>();
        _contendersNames = GetNames(_nameFilePath);

        Shuffle(ref _contendersNames);

        for (var i = 0; i < ContendersNum; i++)
        {
            _contenders.Add(new Contender(_contendersNames[i], i));
        }

        return _contenders;
    }

    private List<string> GetNames(string path)
    {
        // string path = "names.txt";

        var names = new List<string>(ContendersNum);
        var reader = new StreamReader(path);

        for (var i = 0; i < ContendersNum; i++)
        {
            var line = reader.ReadLine();
            if (line == null) throw new IOException("Not enough names in file");
            names.Add(line);
        }

        var repeatedNames = CheckUniqueness(names);
        if (repeatedNames.Count != 0)
        {
            throw new NameListIsNotUniqueException(repeatedNames.ToString());
        }

        reader.Close();
        return names;
    }

    public List<string> CheckUniqueness(List<string> names)
    {
        var repeatedContenders = new List<string>();
        for (var i = 0; i < names.Count - 1; i++)
        {
            for (var j = i + 1; j < names.Count; j++)
            {
                if (names[i] == names[j])
                {
                    repeatedContenders.Add(names[i]);
                }
            }
        }

        return repeatedContenders;
    }

    private static void Shuffle<T>(ref List<T> list)
    {
        var rand = new Random();
        var n = list.Capacity;
        while (n > 1)
        {
            n--;
            var k = rand.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public List<Contender>? GetContenders()
    {
        return _contenders;
    }
}