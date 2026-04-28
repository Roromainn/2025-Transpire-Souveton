using Metier;
using Metier.Interfaces;
using Xunit;

namespace TestMetier;

public class ChargeurDefautTest
{
    private class FakeConsole : IConsole
    {
        public void AfficherGrille(Grille grille) { }
    }

    private Grille CreerGrille()
    {
        var chargeur = new ChargeurDefaut(9);
        var grille = new Grille(9, new FakeConsole(), chargeur);
        grille.Charger();
        return grille;
    }

    [Fact]
    public void ChargerGrilleRetourneTableau()
    {
        var grille = CreerGrille();
        Assert.NotNull(grille.GetCase(0, 0));
    }

    [Fact]
    public void ChargerGrilleCaseVideAfficheAFalse()
    {
        var grille = CreerGrille();
        var c = grille.GetCase(0, 0);
        Assert.Equal(0, c.Valeur);
        Assert.False(c.Affiche);
    }

    [Fact]
    public void ChargerGrilleCaseRemplieAfficheATrue()
    {
        var grille = CreerGrille();
        var c = grille.GetCase(1, 1);
        Assert.Equal(6, c.Valeur);
        Assert.True(c.Affiche);
    }

    [Fact]
    public void ChargerGrilleCoordonneesCorrectes()
    {
        var grille = CreerGrille();
        var c = grille.GetCase(4, 3);
        Assert.Equal(4, c.Ligne);
        Assert.Equal(3, c.Colonne);
    }
}
