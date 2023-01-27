using Lab5.exceptions;
using Lab5.model;
using Lab5.service;

namespace Lab5.service;

public class Hall
{
    private List<Contender> _contenders;
    private int _nextInLine;
    private readonly ContendersGenerator _contendersGenerator;

    public Hall(ContendersGenerator contendersGenerator)
    {
        _contendersGenerator = contendersGenerator;
        _contenders = contendersGenerator.GenerateContenders();
        GenerateQueue();
    }

    public Hall(ContendersGenerator contendersGenerator, string queueFile)
    {
        _contendersGenerator = contendersGenerator;
        _contenders = contendersGenerator.GenerateContenders();
        GenerateQueueFromFile(queueFile);
    }

    public void Reset()
    {
        _nextInLine = 0;
        _contenders = _contendersGenerator.GenerateContenders();
        GenerateQueue();
    }

    public int GetNext()
    {
        if (_nextInLine == _contenders.Count) throw new EndOfQueueException();
        return _nextInLine++;
    }

    public bool HasNext()
    {
        return _nextInLine < _contenders.Count;
    }

    public int WhoIsNext()
    {
        return _nextInLine;
    }

    public void SkipContenders(int number)
    {
        _nextInLine += number;
    }

    private static void Shuffle<T>(ref List<T> list)
    {
        var rand = new Random();
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = rand.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    private void GenerateQueue()
    {
        Shuffle(ref _contenders);
    }

    private void GenerateQueueFromFile(string path)
    {
        var reader = new StreamReader(path);
        var queue = new List<int>();
        for (var i = 0; i < _contenders.Count; i++)
        {
            var line = reader.ReadLine();
            if (line == null) throw new IOException("Not enough lines in file");
            queue.Add(Convert.ToInt32(line));
        }

        for (var i = 0; i < _contenders.Count; i++)
        {
            _contenders[i].PlaceInRate = queue[i];
        }
    }

    public List<Contender> GetContenders()
    {
        return _contenders;
    }

    public void SetNewQueuedContenders(List<Contender> contenders, int nextInLine)
    {
        _nextInLine = nextInLine;
        _contenders = contenders;
    }
}