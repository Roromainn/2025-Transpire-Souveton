using Metier.Interfaces;

namespace Metier;

public class ChargeurDefaut : IChargeur
{
    private int taille;

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

    public int Taille { get { return taille; } }    

    public ChargeurDefaut(int taille)
    {
        this.taille = taille;
    }

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
}
