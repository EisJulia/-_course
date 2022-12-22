namespace Lab1;

public class Princess
{
    private readonly Friend _friend;
    private readonly int _contendersNum;
    public Princess(Friend friend, int contendersNum)
    {
        _friend = friend;
        _contendersNum = contendersNum;
    }

    public int Choose()
    {
        var t = 33;
        // var t = 33;
        // var t = (int)Math.Round(_contendersNum / Math.E);
        for (var i = t; i < _contendersNum - 1; i++)
        {
            var rate = _friend.GetRate(i);
            if (i < 51 && rate == 0) return i;
            if (i >= 51 && i < 65 && rate == 2) return i;
            if (i >= 65 && i < 76 && rate == 2) return i;
            if (i >= 76 && i < 86 && rate == 2) return i;
            if (i >= 86 && rate == 4) return i;
            // if (_friend.IsHeTheBest(i) && _friend.GetRate(i) < 50)
            // {
            //     return i;
            // }
        }

        if (_friend.GetRate(_contendersNum - 1) >= 50)
        {
            return -1;
        }

        return _contendersNum - 1;
    }

    public int GetHappiness(int rate)
    {
        if (rate == -1) return 10;
        if (rate == 0) return 20;
        if (rate == 2) return 50;
        if (rate == 4) return 100;
        return 0;
    }
}