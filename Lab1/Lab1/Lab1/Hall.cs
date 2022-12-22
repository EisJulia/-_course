namespace Lab1;

public class Hall
{
    private readonly int _contendersNum;
    private int[] _contenders;
    private string[] _contendersNames;

    public Hall(int contendersNum)
    {
        _contendersNum = contendersNum;
        GenerateContenders(contendersNum);
    }

    private void GenerateContenders(int contendersNum)
    {
        _contenders = new int[contendersNum];
        _contendersNames = GetNames();

        for (var i = 0; i < _contenders.Length; i++)
        {
            _contenders[i] = i;
        }

        Shuffle(ref _contenders);
        Shuffle(ref _contendersNames);
    }

    private string[] GetNames()
    {
        string path = "names.txt";

        var names = new string[_contendersNum];
        var reader = new StreamReader(path);
        string? line;

        for (var i = 0; i < _contendersNum; i++)
        {
            line = reader.ReadLine();
            names[i] = line ?? throw new IOException();
            //Console.WriteLine(line);
        }

        return names;
    }

    private static void Shuffle<T>(ref T[] array)
    {
        var rand = new Random();
        var n = array.Length;
        while (n > 1)
        {
            n--;
            var k = rand.Next(n + 1);
            (array[k], array[n]) = (array[n], array[k]);
        }
    }

    public int[] GetContenders()
    {
        return _contenders;
    }

    public string[] GetContendersNames()
    {
        return _contendersNames;
    }

    public int GetContenderRate(int number)
    {
        if (number < 0 || number > _contendersNum) return -1;
        return _contenders[number];
    }

    public string GetContenderName(int number)
    {
        return _contendersNames[number];
    }
}