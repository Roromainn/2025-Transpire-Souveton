using Metier.ExepMetier;

namespace Metier.Model;

public class Coordonnes
{
    private int taille;
    private int ligne;
    private int colonne;

    public int Ligne
    {
        get { return ligne; }
        set
        {
            if (value < 0 || value >= taille)
                throw new ECoordonnes($"Ligne invalide : {value}");
            ligne = value;
        }
    }

    public int Colonne
    {
        get { return colonne; }
        set
        {
            if (value < 0 || value >= taille)
                throw new ECoordonnes($"Colonne invalide : {value}");
            colonne = value;
        }
    }

    public Coordonnes(int taille)
    {
        this.taille = taille;
        this.ligne = 0;
        this.colonne = 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Coordonnes other)
        {
            return this.ligne == other.ligne && this.colonne == other.colonne;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ligne, colonne);
    }
}