using Creator.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestCreator;

[TestFixture]
public class GenerateurGrilleTest
{
    [TestCase(4)]
    [TestCase(9)]
    [TestCase(16)]
    public void TestGrilleVide(int taille)
    {
        GenerateurGrille generateur = new GenerateurGrille(taille);
        int[,] grille = generateur.GrilleVide();

        Assert.That(grille.GetLength(0), Is.EqualTo(taille));
        Assert.That(grille.GetLength(1), Is.EqualTo(taille));

        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                Assert.That(grille[i, j], Is.EqualTo(0));
            }
        }
    }

    [Test]
    public void TestCaseSuivante()
    {
        GenerateurGrille generateur = new GenerateurGrille(9);

        (int, int)? case1 = generateur.CaseSuivante(0, 0);
        Assert.That(case1, Is.EqualTo((0, 1)));

        (int, int)? case2 = generateur.CaseSuivante(0, 8);
        Assert.That(case2, Is.EqualTo((1, 0)));

        (int, int)? caseDerniere = generateur.CaseSuivante(8, 8);
        Assert.That(caseDerniere, Is.Null);
    }

    [Test]
    public void TestCalculerValeurPossibles()
    {
        GenerateurGrille generateur = new GenerateurGrille(9);
        int[,] grille = generateur.GrilleVide();

        List<int> possibles = generateur.CalculerValeurPossibles(grille, 0, 0);
        Assert.That(possibles.Count, Is.EqualTo(9));
        for (int i = 1; i <= 9; i++)
        {
            Assert.That(possibles, Contains.Item(i));
        }
    }
}
