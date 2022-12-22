namespace Lab2;

public class Princess
{
    private readonly Friend _friend;

    private readonly Hall _hall;

    public Princess(Friend friend, Hall hall)
    {
        _friend = friend;
        _hall = hall;
    }

    public int Choose(int contendersNum)
    {
        var t = (int)Math.Round(contendersNum / Math.E);
        _hall.SkipContenders(t);
        while (_hall.HasNext())
        {
            var contNum = _hall.GetNext();
            if (_friend.IsHeTheBest(contNum))
            {
                return contNum;
            }
        }
        
        if (_friend.GetRate(contendersNum - 1) >= 50)
        {
            return -1;
        }

        return contendersNum - 1;
    }

    public static int GetHappiness(int rate)
    {
        if (rate == -1) return 10;
        if (rate >= 50) return 0;
        return 100 - rate;
    }
}