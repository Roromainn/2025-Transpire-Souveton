using Metier.Model;
using Xunit;

namespace TestMetier;

public class CaseTest
{
    [Fact]
    public void ConstructeurValeur()
    {
        Case c = new Case(3, 5, 7, true, true);
        Assert.Equal(3, c.Ligne);
        Assert.Equal(5, c.Colonne);
        Assert.Equal(7, c.Valeur);
        Assert.True(c.Affiche);
        Assert.True(c.Initiale);
    }

    [Fact]
    public void ConstructeurAfficheAFalse()
    {
        Case c = new Case(0, 0, 0, false, false);
        Assert.False(c.Affiche);
        Assert.Equal(0, c.Valeur);
        Assert.False(c.Initiale);
    }
}