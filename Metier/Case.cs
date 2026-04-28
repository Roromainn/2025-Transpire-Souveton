namespace Metier;

/// <summary>
/// Classe qui représente une case de la grille de sudoku
/// </summary>
public class Case
{
    #region--Attributs--
    /// <summary>
    /// Ligne de la case
    /// </summary>
    private int ligne;
    /// <summary>
    /// Colonne de la case
    /// </summary>
    private int colonne;
    /// <summary>
    /// Valeur de la case
    /// </summary>
    private int valeur;
    /// <summary>
    /// Indique si la case est affichée
    /// </summary>
    private bool affiche;
    #endregion

    #region--Propriétés--
    public int Ligne { get { return ligne; } }
    public int Colonne { get { return colonne; } }
    public int Valeur { get { return valeur; } }
    public bool Affiche { get { return affiche; } }
    #endregion

    #region--Constructeurs--
    public Case(int ligne, int colonne, int valeur, bool affiche)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        this.valeur = valeur;
        this.affiche = affiche;
    }
    #endregion
}
