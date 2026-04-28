using Metier.ExepMetier;
using Metier.Interfaces;
using Metier.Model;
using Xunit;

namespace TestMetier;

public class GrilleTest
{
    private class FakeChargeur : IChargeur
    {
        public Case[,] ChargerGrille(Grille grille)
        {
            var cases = new Case[grille.Taille, grille.Taille];
            for (int l = 0; l < grille.Taille; l++)
                for (int c = 0; c < grille.Taille; c++)
                    cases[l, c] = new Case(l, c, 1, true);
            return cases;
        }
    }

    private class FakeConsole : IConsole
    {
        public bool AfficherAppele { get; private set; }
        public void AfficherGrille(Grille grille) => AfficherAppele = true;
    }

    private Grille CreerGrille(FakeConsole? console = null, FakeChargeur? chargeur = null)
        => new Grille(9, console ?? new FakeConsole(), chargeur ?? new FakeChargeur());

    [Fact]
    public void ConstructeurTailleValide()
    {
        var g = CreerGrille();
        Assert.Equal(9, g.Taille);
    }

    [Fact]
    public void ConstructeurTailleInvalide()
    {
        Assert.Throws<EGrilleTaille>(() => new Grille(5, new FakeConsole(), new FakeChargeur()));
    }

    [Fact]
    public void GetCaseSansCharger()
    {
        var g = CreerGrille();
        Assert.Throws<EGrilleCharge>(() => g.GetCase(0, 0));
    }

    [Fact]
    public void GetCaseCoordonneesNegatives()
    {
        var g = CreerGrille();
        g.Charger();
        Assert.Throws<EGrilleCoordonnees>(() => g.GetCase(-1, 0));
    }

    [Fact]
    public void GetCaseCoordonneesHorsBornes()
    {
        var g = CreerGrille();
        g.Charger();
        Assert.Throws<EGrilleCoordonnees>(() => g.GetCase(9, 0));
        Assert.Throws<EGrilleCoordonnees>(() => g.GetCase(0, 9));
    }

    [Fact]
    public void GetCaseApresCharger()
    {
        var g = CreerGrille();
        g.Charger();
        var c = g.GetCase(3, 4);
        Assert.Equal(3, c.Ligne);
        Assert.Equal(4, c.Colonne);
    }

    [Fact]
    public void AfficherSansCharger()
    {
        var g = CreerGrille();
        Assert.Throws<EGrilleCharge>(() => g.Afficher());
    }

    [Fact]
    public void AfficherApresCharger()
    {
        var console = new FakeConsole();
        var g = CreerGrille(console: console);
        g.Charger();
        g.Afficher();
        Assert.True(console.AfficherAppele);
    }
}
