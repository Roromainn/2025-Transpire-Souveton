using Metier.ExepMetier;
using Metier.Interfaces;

namespace Metier.Model;

/// <summary>
/// Classe qui represnete les grilles de sudoku
/// </summary>
public class Grille
{
    #region--Attributs--
    /// <summary>
    /// Taille de la grille
    /// </summary>
    private int taille;
    /// <summary>
    /// Tableau de cases de la grille
    /// </summary>
    private Case[,]? cases;
    /// <summary>
    /// Chargeur de cases de la grille
    /// </summary>
    private IChargeur chargeur;
    /// <summary>
    /// Console d'affichage de la grille
    /// </summary>
    private IConsole console;
    #endregion

    #region--Propriétés--
    public int Taille { get { return taille; } }
    public IChargeur Chargeur { get { return chargeur; } }
    public IConsole Console { get { return console; } }
    #endregion

    #region--Contructeurs--
    public Grille(int taille, IConsole console, IChargeur chargeur)
    {
        if (taille != 9)
            throw new EGrilleTaille($"La taille doit être 9, valeur reçue : {taille}");
        this.taille = taille;
        this.console = console;
        this.chargeur = chargeur;
    }
    #endregion

    #region--Méthodes--
    /// <summary>
    /// Méthode qui retourne la case à la position donnée, ou lève une exception si la grille n'est pas chargée ou si les coordonnées sont invalides
    /// </summary>
    /// <param name="ligne">Ligne de la case</param>
    /// <param name="colonne">Colonne de la case</param>
    /// <returns>La case à la position donnée</returns>
    /// <exception cref="EGrilleCharge">Si la grille n'est pas chargée</exception>
    /// <exception cref="EGrilleCoordonnees">Si les coordonnées sont invalides</exception>
    public Case GetCase(int ligne, int colonne)
    {
        if (cases == null)
            throw new EGrilleCharge("La grille n'est pas chargée");
        if (ligne < 0 || ligne >= taille || colonne < 0 || colonne >= taille)
            throw new EGrilleCoordonnees($"Coordonnées invalides : ligne={ligne}, colonne={colonne}");
        return cases[ligne, colonne];
    }

    /// <summary>
    /// Charge la grille
    /// </summary>
    public void Charger()
    {
        cases = chargeur.ChargerGrille(this);
    }
    
    /// <summary>
    /// Affiche la grille
    /// </summary>
    /// <exception cref="EGrilleCharge">Echec du chargement de la grille</exception>
    public void Afficher()
    {
        if (cases == null)
            throw new EGrilleCharge("La grille n'est pas chargée");
        console.AfficherGrille(this);
    }
    #endregion

}
