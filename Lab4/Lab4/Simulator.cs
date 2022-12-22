using Lab4.exceptions;
using Microsoft.EntityFrameworkCore;

namespace Lab4;

public class Simulator
{
    private readonly AttemptContext _db;

    public Simulator(AttemptContext db)
    {
        _db = db;
    }
    
    public Simulator()
    {
        _db = new AttemptContext();
    }

    public void RunAttempt(int id, ref int chosenNum, ref int happiness)
    {
        var attempt = _db.Attempts.Find(id);
        if (attempt == null) throw new NoSuchAttemptException();
        var generator = new ContendersGenerator();
        var hall = new Hall(generator);
        hall.SetNewQueuedContenders(Contender.MapToContenderList(attempt.ContendersNames, attempt.ContendersRates));
        var friend = new Friend(hall);
        var princess = new Princess(friend, hall);
        chosenNum = princess.Choose(100);
        if (chosenNum != attempt.ChosenNumber) throw new SimulationException("Chosen numbers don't match");
        happiness = Princess.GetHappiness(chosenNum);
        if (happiness != attempt.Happiness) throw new SimulationException("Happiness levels don't match");
    }

    public void GenerateAttempts(int attemptsNumber)
    {
        var generator = new ContendersGenerator();
        var hall = new Hall(generator);
        var friend = new Friend(hall);
        var princess = new Princess(friend, hall);
        for (var i = 1; i < attemptsNumber + 1; i++)
        {
            var chosen = princess.Choose(100);
            var happiness = Princess.GetHappiness(chosen);
            var names = new string?[100];
            var rates = new int[100];
            Contender.DivideList(hall.GetContenders(), ref names, ref rates);
            var attempt = new Attempt
            {
                Id = i,
                ChosenNumber = chosen,
                Happiness = happiness,
                ContendersNames = names,
                ContendersRates = rates
            };
            SaveAttemptToDb(attempt);
            hall.Reset();
            friend.Reset();
        }
    }

    public void RegenerateAttempts(int attemptsNumber)
    {
        var generator = new ContendersGenerator();
        var hall = new Hall(generator);
        var friend = new Friend(hall);
        var princess = new Princess(friend, hall);
        for (var i = 1; i < attemptsNumber + 1; i++)
        {
            var chosen = princess.Choose(100);
            var happiness = Princess.GetHappiness(chosen);
            var names = new string?[100];
            var rates = new int[100];
            Contender.DivideList(hall.GetContenders(), ref names, ref rates);
            var attempt = new Attempt
            {
                Id = i,
                ChosenNumber = chosen,
                Happiness = happiness,
                ContendersNames = names,
                ContendersRates = rates
            };
            RewriteAttemptInDb(attempt);
            hall.Reset();
            friend.Reset();
        }
    }

    private void SaveAttemptToDb(Attempt attempt)
    {
        if (_db.Attempts.Find(attempt.Id) != null) throw new AttemptAlreadyExistException();
        _db.Attempts.Add(attempt);
        _db.SaveChanges();
    }

    private void RewriteAttemptInDb(Attempt attempt)
    {
        var dbAttempt = _db.Attempts.Find(attempt.Id);
        if (dbAttempt == null)
        {
            _db.Attempts.Add(attempt);
            _db.SaveChanges();
            return;
        }

        dbAttempt.ChosenNumber = attempt.ChosenNumber;
        dbAttempt.ContendersNames = attempt.ContendersNames;
        dbAttempt.ContendersRates = attempt.ContendersRates;
        dbAttempt.Happiness = attempt.Happiness;
        _db.Entry(dbAttempt).State = EntityState.Modified;

        _db.SaveChanges();
    }

    public int CalcAvrHappiness()
    {
        return _db.Attempts.Sum(x => x.Happiness) / _db.Attempts.Count();
    }

    private void DumpInFiles(int[] rates, string[] names)
    {
        var ratesWriter = new StreamWriter("D:\\Study\\Lab4\\TestProject\\rates.txt");
        var namesWriter = new StreamWriter("D:\\Study\\Lab4\\TestProject\\names.txt");
        for (var i = 0; i < rates.Length; i++)
        {
            ratesWriter.WriteLine(rates[i]);
            namesWriter.WriteLine(names[i]);
        }
        ratesWriter.Close();
        namesWriter.Close();
    }
}