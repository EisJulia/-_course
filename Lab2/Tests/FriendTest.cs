using Lab2;

namespace Tests;

using NUnit.Framework;

public class FriendTest
{
    [TestCase("D:\\Study\\Lab2\\Tests\\queueDesc.txt", 0, 10, 0)]
    [TestCase("D:\\Study\\Lab2\\Tests\\queueAsc.txt", 0, 4, 3)]
    public void TestFriendComparing(string queuePath, int contenderNum, int contendersPassed, int result)
    {
        var hall = new Hall(new ContendersGenerator(), queuePath);
        hall.SkipContenders(contendersPassed);
        var friend = new Friend(hall);
        Assert.That(result, Is.EqualTo(friend.GetRate(contenderNum)));
    }

    [TestCase(5, 2)]
    public void TestComparingIfOneStillInHall(int contenderNum, int contendersPassed)
    {
        var generator = new ContendersGenerator();
        var hall = new Hall(generator.GenerateContenders());
        hall.SkipContenders(contendersPassed);
        var friend = new Friend(hall);
        Assert.Throws<ContenderStillInHallException>(() => friend.GetRate(contenderNum));
    }
}