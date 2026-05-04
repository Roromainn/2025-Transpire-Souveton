using Metier.ExepMetier;
using Metier.Model;
using Xunit;

namespace TestMetier;

public class CoordonnesTest
{
    [Fact]
    public void ConstructeurEtProprietes()
    {
        Coordonnes c = new Coordonnes(9);
        Assert.Equal(0, c.Ligne);
        Assert.Equal(0, c.Colonne);
        
        c.Ligne = 5;
        c.Colonne = 8;
        Assert.Equal(5, c.Ligne);
        Assert.Equal(8, c.Colonne);
    }

    [Fact]
    public void SettersInvalides()
    {
        Coordonnes c = new Coordonnes(9);
        Assert.Throws<ECoordonnes>(() => c.Ligne = -1);
        Assert.Throws<ECoordonnes>(() => c.Ligne = 9);
        Assert.Throws<ECoordonnes>(() => c.Colonne = -1);
        Assert.Throws<ECoordonnes>(() => c.Colonne = 9);
    }

    [Fact]
    public void EqualsEtHashCode()
    {
        Coordonnes c1 = new Coordonnes(9) { Ligne = 1, Colonne = 2 };
        Coordonnes c2 = new Coordonnes(9) { Ligne = 1, Colonne = 2 };
        Coordonnes c3 = new Coordonnes(9) { Ligne = 3, Colonne = 4 };

        Assert.True(c1.Equals(c2));
        Assert.False(c1.Equals(c3));
        Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        Assert.NotEqual(c1.GetHashCode(), c3.GetHashCode());
    }
}