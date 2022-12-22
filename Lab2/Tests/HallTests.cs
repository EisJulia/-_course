using Lab2;

namespace Tests;

using NUnit.Framework;

public class HallTests
{
    [TestCase(3, 4)]
    public void TestNextContenderCalling(int currentContender, int result)
    {
        var hall = new Hall(new ContendersGenerator());
        hall.SkipContenders(currentContender + 1);

        Assert.That(result, Is.EqualTo(hall.GetNext()));
    }

    [TestCase(100)]
    public void TestOutOfContenders(int contendersNum)
    {
        var hall = new Hall(new ContendersGenerator());
        hall.SkipContenders(contendersNum);

        Assert.Throws<EndOfQueueException>(() => hall.GetNext());
    }
}