using Lab2;

namespace Tests;
using NUnit.Framework;

public class ContenderGeneratorTests
{
    [TestCase("D:\\Study\\Lab2\\Lab2\\names_with_copies.txt")]
    public void TestNameUniqueness(string filepath)
    {
        var generator = new ContendersGenerator(filepath);
        Assert.Throws<NameListIsNotUniqueException>(() => generator.GenerateContenders());
    }
}