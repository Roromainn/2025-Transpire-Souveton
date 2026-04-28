using Metier;
using Metier.Model;
using Xunit;

namespace TestMetier;

public class CaseTest
{
    [Fact]
    public void ConstructeurValeur()
    {
        var c = new Case(3, 5, 7, true);
        Assert.Equal(3, c.Ligne);
        Assert.Equal(5, c.Colonne);
        Assert.Equal(7, c.Valeur);
        Assert.True(c.Affiche);
    }

    [Fact]
    public void ConstructeurAfficheAFalse()
    {
        var c = new Case(0, 0, 0, false);
        Assert.False(c.Affiche);
        Assert.Equal(0, c.Valeur);
    }
}
