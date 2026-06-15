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
    
    private int? valeurSelectionne;
    private Coordonnes curseur;
    private bool choixMode;
    private int erreurs;
    private bool partieTerminee;
    #endregion

    #region--Propriétés--
    public int Taille { get { return taille; } }
    public IChargeur Chargeur { get { return chargeur; } }
    public IConsole Console { get { return console; } }
    public bool Choix { get { return choixMode; } }
    public int Erreurs { get { return erreurs; } }
    public bool PartieTerminee { get { return partieTerminee; } }

    public int? ValeurSelectionne
    {
        get { return valeurSelectionne; }
        set
        {
            if (value == null)
                throw new EGrilleValeur("La valeur ne peut pas être nulle");
            if (value < 1 || value > taille)
                throw new EGrilleValeur($"La valeur doit être comprise entre 1 et {taille}, valeur reçue : {value}");
            valeurSelectionne = value;
        }
    }
    
    public Coordonnes Curseur
    {
        get { return curseur; }
        set { curseur = value; }
    }
    #endregion

    #region--Contructeurs--
    public Grille(int taille, IConsole console, IChargeur chargeur)
    {
        if (taille != 4 && taille != 9 && taille != 16)
            throw new EGrilleTaille($"La taille doit être 4, 9 ou 16, valeur reçue : {taille}");
        this.taille = taille;
        this.console = console;
        this.chargeur = chargeur;
        this.curseur = new Coordonnes(taille);
        this.valeurSelectionne = null;
        this.erreurs = 0;
        this.partieTerminee = false;
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

    public void MettreValeur()
    {
        if (cases == null)
            throw new EGrilleCharge("La grille n'est pas chargée");

        if (valeurSelectionne == null)
            throw new EGrilleValeurNulle("Aucune valeur sélectionnée");

        Case currentCase = cases[curseur.Ligne, curseur.Colonne];
        if (!currentCase.Initiale && !currentCase.Affiche)
        {
            if (currentCase.Valeur == valeurSelectionne.Value)
            {
                currentCase.Reveler();
                currentCase.Choix.Clear();
                if (EstPleine())
                {
                    partieTerminee = true;
                    console.AfficherFin("Vous avez gagné avec " + erreurs + " erreur");
                }
            }
            else
            {
                erreurs++;
                if (erreurs >= 3)
                {
                    partieTerminee = true;
                    console.AfficherFin("Vous avez perdu");
                }
            }
        }
    }

    public void ChangerMode()
    {
        choixMode = !choixMode;
    }

    public void EnleverChoix()
    {
        if (cases == null)
            throw new EGrilleCharge("La grille n'est pas chargée");

        if (valeurSelectionne == null)
            throw new EGrilleValeurNulle("Aucune valeur sélectionnée");

        int racine = (int)Math.Sqrt(taille);
        int ligne = curseur.Ligne;
        int colonne = curseur.Colonne;
        int valeur = valeurSelectionne.Value;

        // Enlever choix de la case du curseur
        cases[ligne, colonne].Choix.Clear();

        // Enlever choix de la ligne
        for (int c = 0; c < taille; c++)
        {
            cases[ligne, c].Choix.Remove(valeur);
        }

        // Enlever choix de la colonne
        for (int l = 0; l < taille; l++)
        {
            cases[l, colonne].Choix.Remove(valeur);
        }

        // Enlever choix de la sous-grille
        int sousLigneDebut = (ligne / racine) * racine;
        int sousColonneDebut = (colonne / racine) * racine;
        for (int l = sousLigneDebut; l < sousLigneDebut + racine; l++)
        {
            for (int c = sousColonneDebut; c < sousColonneDebut + racine; c++)
            {
                cases[l, c].Choix.Remove(valeur);
            }
        }
    }

    private bool EstPleine()
    {
        for (int l = 0; l < taille; l++)
        {
            for (int c = 0; c < taille; c++)
            {
                if (!cases![l, c].Affiche)
                    return false;
            }
        }
        return true;
    }
    #endregion

}
