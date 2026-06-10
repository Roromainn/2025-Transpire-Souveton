namespace Metier.Model;

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
    /// <summary>
    /// Indique si la case est initiale
    /// </summary>
    private bool initiale;
    /// <summary>
    /// Valeurs possibles pour cette case
    /// </summary>
    private List<int> choix;
    #endregion

    #region--Propriétés--
    public int Ligne { get { return ligne; } }
    public int Colonne { get { return colonne; } }
    public int Valeur { get { return valeur; } }
    public bool Affiche { get { return affiche; } }
    public bool Initiale { get { return initiale; } }
    public List<int> Choix { get { return choix; } }
    #endregion

    #region--Constructeurs--
    public Case(int ligne, int colonne, int valeur, bool affiche, bool initiale)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        this.valeur = valeur;
        this.affiche = affiche;
        this.initiale = initiale;
        this.choix = new List<int>();
    }
    #endregion

    #region--Méthodes--
    /// <summary>
    /// Ajoute ou enlève un choix
    /// </summary>
    public void Choisir(int valeur)
    {
        if (choix.Contains(valeur))
            choix.Remove(valeur);
        else
            choix.Add(valeur);
    }

    /// <summary>
    /// Met à jour la valeur et l'affichage
    /// </summary>
    public void SetValeur(int valeur)
    {
        this.valeur = valeur;
        this.affiche = true;
    }
    #endregion
}
