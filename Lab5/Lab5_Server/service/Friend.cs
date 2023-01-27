using Lab5.exceptions;
using Lab5.model;
using Lab5.service;

namespace Lab5.service;

public class Friend
{
    private List<Contender> _contenders;

    private readonly Hall _hall;

    public Friend(Hall hall)
    {
        _hall = hall;
        _contenders = _hall.GetContenders();
    }

    public void Reset()
    {
        _contenders = _hall.GetContenders();
    }

    //Чем меньше тем лучше. Среди отсмотренных
    public bool IsHeTheBest(int number)
    {
        if (number >= _hall.WhoIsNext()) throw new ContenderStillInHallException();
        for (var i = 0; i < number; i++)
        {
            if (_contenders[i].PlaceInRate < _contenders[number].PlaceInRate) return false;
        }

        return true;
    }

    // место в рейтинге, среди отсмотренных
    public int GetRate(int number)
    {
        if (number >= _hall.WhoIsNext()) throw new ContenderStillInHallException();
        var rate = 0;
        for (var i = 0; i < _hall.WhoIsNext(); i++)
        {
            if (_contenders[i].PlaceInRate < _contenders[number].PlaceInRate) rate++;
        }

        return rate;
    }
}