using Lab5.exceptions;
using Lab5.model;
using Lab5.repository;
using Microsoft.EntityFrameworkCore;

namespace Lab5.service;

public class Simulator
{
    private readonly AttemptContext _db;
    
    private int _attemptId = 1;

    private readonly ContendersGenerator _generator;

    public Simulator(AttemptContext db)
    {
        _db = db;
        _generator = new ContendersGenerator();
    }
    
    public Simulator()
    {
        _db = new AttemptContext();
        _generator = new ContendersGenerator();
    }

    public void GenerateByAttempt(out Friend friend, out Hall hall, int attemptId)
    {
        var attempt = _db.Attempts.Find(attemptId);
        if (attempt == null) throw new NoSuchAttemptException();
        hall = new Hall(_generator);
        hall.SetNewQueuedContenders(Contender.MapToContenderList(attempt.ContendersNames, attempt.ContendersRates), 
            attempt.NextInLine);
        friend = new Friend(hall);
    }

    public int GenerateAttempt()
    {
        var generator = new ContendersGenerator();
        var hall = new Hall(generator);
        
        int? chosen = null;
        int? happiness = null;
        var names = new string?[100];
        var rates = new int[100];
        Contender.DivideList(hall.GetContenders(), ref names, ref rates);
        var attempt = new Attempt
        {
            Id = _attemptId,
            ChosenNumber = chosen,
            NextInLine = 0,
            Happiness = happiness,
            ContendersNames = names,
            ContendersRates = rates
        };
        Console.Out.WriteLine(rates);
        SaveAttemptToDb(attempt);
        return _attemptId++;
    }

    public void PrincessChose(int number, int happiness, int attemptId)
    {
        var dbAttempt = _db.Attempts.Find(attemptId);
        if (dbAttempt == null) return;
        dbAttempt.ChosenNumber = number;
        dbAttempt.Happiness = happiness;
        dbAttempt.NextInLine = 0;
        RewriteAttemptInDb(dbAttempt);
    }

    public void RegenerateAttempt(int attemptId)
    {
        var generator = new ContendersGenerator();
        var hall = new Hall(generator);
        
        int? chosen = null;
        int? happiness = null;
        var names = new string?[100];
        var rates = new int[100];
        Contender.DivideList(hall.GetContenders(), ref names, ref rates);
        var attempt = new Attempt
        {
            Id = attemptId,
            ChosenNumber = chosen,
            NextInLine = 0,
            Happiness = happiness,
            ContendersNames = names,
            ContendersRates = rates
        };
        RewriteAttemptInDb(attempt);
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
        dbAttempt.NextInLine = attempt.NextInLine;
        dbAttempt.ContendersNames = attempt.ContendersNames;
        dbAttempt.ContendersRates = attempt.ContendersRates;
        dbAttempt.Happiness = attempt.Happiness;
        _db.Entry(dbAttempt).State = EntityState.Modified;

        _db.SaveChanges();
    }

    public void UpdateAttempt(Hall hall, int attemptId)
    {
        var dbAttempt = _db.Attempts.Find(attemptId);
        if (dbAttempt == null)
        {
            return;
        }

        dbAttempt.NextInLine = hall.WhoIsNext();
        _db.Entry(dbAttempt).State = EntityState.Modified;

        _db.SaveChanges();
    }

    public int? CalcAvrHappiness()
    {
        return _db.Attempts.Sum(x => x.Happiness) / _db.Attempts.Count();
    }
}