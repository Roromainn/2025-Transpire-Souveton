using Creator.ExepCreator;
using Metier.Interfaces;
using Metier.Model;

namespace Creator.Model;

/// <summary>
/// Chargeur de grilles Sudoku générées aléatoirement
/// </summary>
public class ChargeurHasard : IChargeur
{
    private int taille;
    private int difficulte;

    /// <summary>
    /// Constructeur du chargeur hasard
    /// </summary>
    /// <param name="taille">Taille de la grille (4, 9 ou 16)</param>
    /// <param name="difficulte">Difficulté de 1 à 6</param>
    /// <exception cref="EchargeurHasardTaille">Si taille n'est pas 4, 9 ou 16</exception>
    /// <exception cref="EchargeurHasardDifficulte">Si difficulté n'est pas entre 1 et 6</exception>
    public ChargeurHasard(int taille, int difficulte)
    {
        if (taille != 4 && taille != 9 && taille != 16)
            throw new EchargeurHasardTaille($"Taille invalide : {taille}. Doit être 4, 9 ou 16");

        if (difficulte < 1 || difficulte > 6)
            throw new EchargeurHasardDifficulte($"Difficulté invalide : {difficulte}. Doit être entre 1 et 6");

        this.taille = taille;
        this.difficulte = difficulte;
    }

    public Case[,] ChargerGrille(Grille grille)
    {
        GenerateurGrille generateur = new GenerateurGrille(taille);
        Random random = new Random();

        //creer une grille 
        int[,] grilleComplete = generateur.GrilleVide();
        generateur.RemplirGrille(grilleComplete, 0, 0);

        int[,] grilleFinal = (int[,])grilleComplete.Clone();

        // Liste des cases disponibles
        List<(int, int)> casesDisponibles = new List<(int, int)>();
        for (int l = 0; l < taille; l++)
        {
            for (int c = 0; c < taille; c++)
            {
                casesDisponibles.Add((l, c));
            }
        }

        int casesSupprimes = 0;
        int casesASupprimer = difficulte * 10;
        bool encorePossible = true;

        while (casesSupprimes < casesASupprimer && encorePossible)
        {
            List<(int, int)> casesRestantesTour = new List<(int, int)>(casesDisponibles);
            bool boucleCase = true;

            while (boucleCase && casesRestantesTour.Count > 0)
            {
                int index = random.Next(casesRestantesTour.Count);
                (int ligne, int colonne) = casesRestantesTour[index];
                casesRestantesTour.RemoveAt(index);
                
                int valeurSauvegardee = grilleFinal[ligne, colonne];
                grilleFinal[ligne, colonne] = 0;

                int solutions = generateur.CalculerNombreSolutions(grilleFinal, 0, 0);

                if (solutions == 1)
                {
                    //Case bonne, on la garde vide
                    casesSupprimes++;
                    casesDisponibles.Remove((ligne, colonne));
                    boucleCase = false;
                }
                else
                {
                    //remettre la valeur
                    grilleFinal[ligne, colonne] = valeurSauvegardee;
                }

                if (casesRestantesTour.Count == 0)
                {
                    encorePossible = false;
                    boucleCase = false;
                }
            }
        }

        return ConvertirEnCases(grilleComplete, grilleFinal);
    }

    /// <summary>
    /// Convertit en Case[,] : la valeur stockée est la solution,
    /// affiche/initiale dépend si la case est un indice de départ (non supprimée).
    /// </summary>
    private Case[,] ConvertirEnCases(int[,] solution, int[,] grilleFinal)
    {
        Case[,] cases = new Case[taille, taille];
        for (int l = 0; l < taille; l++)
        {
            for (int c = 0; c < taille; c++)
            {
                int valeur = solution[l, c];
                bool initiale = grilleFinal[l, c] != 0;
                cases[l, c] = new Case(l, c, valeur, initiale, initiale);
            }
        }
        return cases;
    }
}
