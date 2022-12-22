namespace Lab1;

public class Friend
{
    private readonly int[] _contenders;

    public Friend(int[] contenders)
    {
        _contenders = contenders;
    }

    //The less the better
    public bool IsHeTheBest(int number)
    {
        for (var i = 0; i < number; i++)
        {
            if (_contenders[i] < _contenders[number]) return false;
        }

        return true;
    }

    // место в рейтинге, среди отсмотренных
    public int GetRate(int number)
    {
        var rate = 0;
        for (var i = 0; i < number; i++)
        {
            if (_contenders[i] < _contenders[number]) rate++;
        }

        return rate;
    }
}