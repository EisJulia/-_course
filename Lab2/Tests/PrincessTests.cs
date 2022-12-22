using Lab2;
using NUnit.Framework;

namespace Tests;

public class PrincessTests
{
    [TestCase("D:\\Study\\Lab2\\Tests\\queueDesc.txt", -1)]
    [TestCase("D:\\Study\\Lab2\\Tests\\queueAsc.txt", 37)]
    [TestCase("D:\\Study\\Lab2\\Tests\\queueMixed.txt", 47)]
    public void TestStrategy(string queuePath, int result)
    {
        var hall = new Hall(new ContendersGenerator(), queuePath);
        var princess = new Princess(new Friend(hall), hall);
        Assert.That(result, Is.EqualTo(princess.Choose(100)));
    }
}
