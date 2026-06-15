using Creator.ExepCreator;
using Creator.Model;
using NUnit.Framework;

namespace TestCreator;

[TestFixture]
public class ChargeurHasardTest
{
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(8)]
    [TestCase(10)]
    [TestCase(25)]
    public void TestConstructorTailleInvalide(int taille)
    {
        Assert.Throws<EchargeurHasardTaille>(() => new ChargeurHasard(taille, 3));
    }

    [TestCase(4)]
    [TestCase(9)]
    [TestCase(16)]
    public void TestConstructorTailleValide(int taille)
    {
        Assert.DoesNotThrow(() => new ChargeurHasard(taille, 3));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(10)]
    public void TestConstructorDifficultéInvalide(int difficulte)
    {
        Assert.Throws<EchargeurHasardDifficulte>(() => new ChargeurHasard(9, difficulte));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public void TestConstructorDifficultéValide(int difficulte)
    {
        Assert.DoesNotThrow(() => new ChargeurHasard(9, difficulte));
    }
}
