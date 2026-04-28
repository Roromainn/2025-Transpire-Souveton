using Metier.Interfaces;

namespace Metier;

/// <summary>
/// Classe qui charge la grille de sudoku par défaut
/// </summary>
public class ChargeurDefaut : IChargeur
{
    #region--Attributs--
    /// <summary>
    /// Taille de la grile
    /// </summary>
    private int taille;

    /// <summary>
    /// Grille par defaut du tp
    /// </summary>
    private int[,] GrilleDefaut = new int[9, 9]
    {
        { 0, 0, 0,  0, 0, 3,  0, 0, 2 },
        { 0, 6, 7,  0, 0, 0,  0, 3, 0 },
        { 0, 9, 0,  8, 0, 0,  0, 0, 4 },

        { 2, 0, 3,  1, 0, 7,  0, 0, 0 },
        { 6, 0, 0,  4, 0, 0,  0, 0, 0 },
        { 1, 0, 0,  6, 0, 0,  0, 5, 7 },

        { 0, 0, 1,  2, 0, 0,  5, 0, 8 },
        { 0, 2, 0,  0, 0, 0,  9, 0, 0 },
        { 0, 0, 0,  0, 0, 0,  0, 7, 3 },
    };
    #endregion

    #region--Propriétés--
    public int Taille { get { return taille; } }
    #endregion

    #region--Constructeurs--
    public ChargeurDefaut(int taille)
    {
        this.taille = taille;
    }
    #endregion


    #region--Méthodes--
    /// <summary>
    /// Charge la grille de sudoku par défaut et retourne un tableau de cases
    /// </summary>
    /// <param name="grille">grille a charger</param>
    /// <returns>tableau de cases</returns>
    public Case[,] ChargerGrille(Grille grille)
    {
        var cases = new Case[taille, taille];
        for (int l = 0; l < taille; l++)
            for (int c = 0; c < taille; c++)
            {
                int val = GrilleDefaut[l, c];
                cases[l, c] = new Case(l, c, val, val != 0);
            }
        return cases;
    }
    #endregion
}
