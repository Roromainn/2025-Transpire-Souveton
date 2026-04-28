namespace Metier;

public class Case
{
    private int ligne;

    private int colonne;

    private int valeur;

    private bool affiche;

    public int Ligne { get { return ligne; } }
    public int Colonne { get { return colonne; } }
    public int Valeur { get { return valeur; } }
    public bool Affiche { get { return affiche; } }

    public Case(int ligne, int colonne, int valeur, bool affiche)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        this.valeur = valeur;
        this.affiche = affiche;
    }
}
