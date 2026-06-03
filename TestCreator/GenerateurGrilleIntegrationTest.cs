using Creator.Model;
using Metier.Interfaces;
using Metier.Model;
using NUnit.Framework;

namespace TestCreator;

[TestFixture]
public class GenerateurGrilleIntegrationTest
{
    [TestCase(9, 1)]
    [TestCase(9, 2)]
    [TestCase(9, 3)]
    public void TestChargeurHasardGenereGrilleValide(int taille, int difficulte)
    {
        IChargeur chargeur = new ChargeurHasard(taille, difficulte);
        IConsole console = new ConsoleTest();
        Grille grille = new Grille(taille, console, chargeur);

        grille.Charger();

        // Vérifier que la grille est chargée
        for (int l = 0; l < taille; l++)
        {
            for (int c = 0; c < taille; c++)
            {
                Case cas = grille.GetCase(l, c);
                Assert.That(cas.Valeur, Is.GreaterThanOrEqualTo(0));
                Assert.That(cas.Valeur, Is.LessThanOrEqualTo(taille));
            }
        }
    }

    [Test]
    public void TestRemplirGrilleComplete()
    {
        // Arrange
        GenerateurGrille generateur = new GenerateurGrille(9);
        int[,] grille = generateur.GrilleVide();

        // Act
        bool result = generateur.RemplirGrille(grille, 0, 0);

        // Assert
        Assert.That(result, Is.True);

        // Vérifier qu'aucune case n'est vide
        for (int l = 0; l < 9; l++)
        {
            for (int c = 0; c < 9; c++)
            {
                Assert.That(grille[l, c], Is.GreaterThan(0));
                Assert.That(grille[l, c], Is.LessThanOrEqualTo(9));
            }
        }
    }
}


