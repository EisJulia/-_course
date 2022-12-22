using Lab1;

var happinessSum = 0;
const int contendersNum = 100;
const int iterNum = 1000;
for (var i = 0; i < iterNum; i++)
{
    var hall = new Hall(contendersNum);
    var friend = new Friend(hall.GetContenders());
    var princess = new Princess(friend, contendersNum);
    var number = princess.Choose();
    var happiness = princess.GetHappiness(hall.GetContenderRate(number));
    happinessSum += happiness;
    var names = hall.GetContendersNames();
    var contenders = hall.GetContenders();
    // for (var j = 0; j < contendersNum; j++)
    // {
    //     Console.WriteLine(names[j] + " " + contenders[j]);
    // }

    if (hall.GetContenderRate(number) < 5)
    {
        Console.Write(hall.GetContenderRate(number) + 1);
        Console.WriteLine(" happiness:" + happiness);
    }
}

Console.WriteLine("avr happiness: " + happinessSum / iterNum);

// var strategyCalculator = new StrategyCalculator();
// strategyCalculator.Calculate();
// strategyCalculator.WriteResultsInFiles();