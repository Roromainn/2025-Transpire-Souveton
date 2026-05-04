using Metier.Interfaces;
using Metier.Model;
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
        ChargeurDefaut chargeur = new ChargeurDefaut(9);
        Grille grille = new Grille(9, new FakeConsole(), chargeur);
        grille.Charger();
        return grille;
    }

    [Fact]
    public void ChargerGrilleRetourneTableau()
    {
        Grille grille = CreerGrille();
        Assert.NotNull(grille.GetCase(0, 0));
    }

    [Fact]
    public void ChargerGrilleCaseCacheeAfficheAFalse()
    {
        Grille grille = CreerGrille();
        Case c = grille.GetCase(0, 0);
        Assert.Equal(8, c.Valeur);
        Assert.False(c.Affiche);
        Assert.False(c.Initiale);
    }

    [Fact]
    public void ChargerGrilleCaseRemplieAfficheATrue()
    {
        Grille grille = CreerGrille();
        Case c = grille.GetCase(1, 1);
        Assert.Equal(6, c.Valeur);
        Assert.True(c.Affiche);
        Assert.True(c.Initiale);
    }

    [Fact]
    public void ChargerGrilleCoordonneesCorrectes()
    {
        Grille grille = CreerGrille();
        Case c = grille.GetCase(4, 3);
        Assert.Equal(4, c.Ligne);
        Assert.Equal(3, c.Colonne);
    }
}