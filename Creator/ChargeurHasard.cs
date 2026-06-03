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
        throw new NotImplementedException();
    }
}
