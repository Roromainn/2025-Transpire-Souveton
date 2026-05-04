using Metier.Interfaces;
using System;

namespace Metier.Model;

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
    /// Grille par defaut du tp2 (Page 4/6)
    /// Les valeurs positives sont initiales.
    /// </summary>
    private int[,] GrilleDefaut = new int[9, 9]
    {
        { -8, -1, -5, -7, -4, +3, -6, -9, +2 },
        { -4, +6, +7, -9, -2, -1, -8, +3, -5 },
        { -3, +9, -2, +8, -5, -6, -7, -1, +4 },
        { +2, -5, +3, +1, -9, +7, -4, -8, -6 },
        { +6, -7, -8, +4, -3, -5, -1, -2, -9 },
        { +1, -4, -9, +6, -8, -2, -3, +5, +7 },
        { -7, -3, +1, +2, -6, -9, +5, -4, +8 },
        { -5, +2, -4, -3, -7, -8, +9, -6, -1 },
        { -9, -8, -6, -5, -1, -4, -2, +7, +3 }
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
        Case[,] cases = new Case[taille, taille];
        for (int l = 0; l < taille; l++)
            for (int c = 0; c < taille; c++)
            {
                int val = GrilleDefaut[l, c];
                bool initiale = val > 0;
                int vraieValeur = Math.Abs(val);
                // Si initiale est vrai, alors affiche est vrai aussi.
                cases[l, c] = new Case(l, c, vraieValeur, initiale, initiale);
            }
        return cases;
    }
    #endregion
}