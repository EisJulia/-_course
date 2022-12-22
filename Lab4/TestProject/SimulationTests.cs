using Lab4;
using Lab4.exceptions;
using Microsoft.EntityFrameworkCore;

namespace TestProject;

public class Tests
{
    [SetUp]
    public void InitDb()
    {
        var db = new AttemptContext();
        db.Database.EnsureDeleted();
        db.SaveChanges();
    }
    
    [TestCase(38, 62, "D:\\Study\\Lab4\\TestProject\\rates.txt")]
    [TestCase(10, 62, "D:\\Study\\Lab4\\TestProject\\rates.txt")]
    [TestCase(38, 80, "D:\\Study\\Lab4\\TestProject\\rates.txt")]
    public void RunSimulationTest(int chosen, int happiness, string ratesPath)
    {
        var db = new AttemptContext();
        var rates = ReadRates(ratesPath);
        var attempt = new Attempt
        {
            Id = 1,
            ChosenNumber = chosen,
            Happiness = happiness,
            ContendersNames = null,
            ContendersRates = rates
        };
        db.Attempts.Add(attempt);
        db.SaveChanges();

        var simulator = new Simulator(db);
        int newChosen = -1, newHappiness = 0;
        try
        {
            simulator.RunAttempt(1, ref newChosen, ref newHappiness);
        }
        catch (SimulationException e)
        {
            if (newChosen != chosen)
            {
                Assert.That(e.Message, Is.EqualTo("Chosen numbers don't match"));
                return;
            }

            if (newHappiness != happiness)
            {
                Assert.That(e.Message, Is.EqualTo("Happiness levels don't match"));
                return;
            }
        }
        Assert.That(newChosen == chosen && newHappiness == happiness);
    }

    [Test]
    public void Generate100Test()
    {
        var db = new AttemptContext();
        var simulator = new Simulator(db);
        simulator.GenerateAttempts(100);
        Assert.That(db.Attempts.Count(), Is.EqualTo(100));
    }

    public int[] ReadRates(string path)
    {
        var reader = new StreamReader(path);
        var rates = new int[100];

        for (var i = 0; i < 100; i++)
        {
            var line = reader.ReadLine();
            if (line == null) throw new IOException("Not enough rates in file");
            rates[i] = Int32.Parse(line);
        }

        return rates;
    }

    public string[] ReadNames(string path)
    {
        var reader = new StreamReader(path);
        var names = new string[100];
        for (var i = 0; i < 100; i++)
        {
            var line = reader.ReadLine();
            names[i] = line ?? throw new IOException("Not enough names in file");
        }

        return names;
    }
}