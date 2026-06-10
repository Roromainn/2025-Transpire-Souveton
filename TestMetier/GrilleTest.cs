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
            Case[,] cases = new Case[grille.Taille, grille.Taille];
            for (int l = 0; l < grille.Taille; l++)
                for (int c = 0; c < grille.Taille; c++)
                {
                    bool init = (l == 0 && c == 0);
                    cases[l, c] = new Case(l, c, 1, init, init);
                }
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
        Grille g = CreerGrille();
        Assert.Equal(9, g.Taille);
        Assert.Null(g.ValeurSelectionne);
        Assert.NotNull(g.Curseur);
    }

    [Fact]
    public void ConstructeurTailleInvalide()
    {
        Assert.Throws<EGrilleTaille>(() => new Grille(5, new FakeConsole(), new FakeChargeur()));
    }

    [Fact]
    public void ValeurSelectionneValide()
    {
        Grille g = CreerGrille();
        g.ValeurSelectionne = 5;
        Assert.Equal(5, g.ValeurSelectionne);
    }

    [Fact]
    public void ValeurSelectionneInvalide()
    {
        Grille g = CreerGrille();
        Assert.Throws<EGrilleValeur>(() => g.ValeurSelectionne = 0);
        Assert.Throws<EGrilleValeur>(() => g.ValeurSelectionne = 10);
        Assert.Throws<EGrilleValeur>(() => g.ValeurSelectionne = null);
    }

    [Fact]
    public void MettreValeurSucces()
    {
        Grille g = CreerGrille();
        g.Charger();
        // 1 = solution de (1,1) => révélée
        g.ValeurSelectionne = 1;
        g.Curseur.Ligne = 1;
        g.Curseur.Colonne = 1;

        g.MettreValeur();
        Case c = g.GetCase(1, 1);
        Assert.True(c.Affiche);
        Assert.Equal(1, c.Valeur);
    }

    [Fact]
    public void MettreValeurMauvaiseValeur()
    {
        Grille g = CreerGrille();
        g.Charger();
        g.ValeurSelectionne = 2;
        g.Curseur.Ligne = 1;
        g.Curseur.Colonne = 1;

        g.MettreValeur();
        Case c = g.GetCase(1, 1);
        Assert.False(c.Affiche);
    }

    [Fact]
    public void MettreValeurSurInitiale()
    {
        Grille g = CreerGrille();
        g.Charger();
        g.ValeurSelectionne = 1;
        g.Curseur.Ligne = 0;
        g.Curseur.Colonne = 0;
        g.MettreValeur();
    }

    [Fact]
    public void MettreValeurSansSelection()
    {
        Grille g = CreerGrille();
        g.Charger();
        Assert.Throws<EGrilleValeurNulle>(() => g.MettreValeur());
    }

    [Fact]
    public void GetCaseSansCharger()
    {
        Grille g = CreerGrille();
        Assert.Throws<EGrilleCharge>(() => g.GetCase(0, 0));
    }

    [Fact]
    public void GetCaseCoordonneesNegatives()
    {
        Grille g = CreerGrille();
        g.Charger();
        Assert.Throws<EGrilleCoordonnees>(() => g.GetCase(-1, 0));
    }

    [Fact]
    public void GetCaseCoordonneesHorsBornes()
    {
        Grille g = CreerGrille();
        g.Charger();
        Assert.Throws<EGrilleCoordonnees>(() => g.GetCase(9, 0));
        Assert.Throws<EGrilleCoordonnees>(() => g.GetCase(0, 9));
    }

    [Fact]
    public void AfficherSansCharger()
    {
        Grille g = CreerGrille();
        Assert.Throws<EGrilleCharge>(() => g.Afficher());
    }

    [Fact]
    public void AfficherApresCharger()
    {
        FakeConsole console = new FakeConsole();
        Grille g = CreerGrille(console: console);
        g.Charger();
        g.Afficher();
        Assert.True(console.AfficherAppele);
    }
}