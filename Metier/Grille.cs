using Metier.ExepMetier;
using Metier.Interfaces;

namespace Metier;

public class Grille
{
    private int taille;
    public int Taille { get { return taille; } }
    private Case[,]? cases;
    private readonly IChargeur chargeur;
    private readonly IConsole console;

    public Grille(int taille, IConsole console, IChargeur chargeur)
    {
        if (taille != 9)
            throw new EGrilleTaille($"La taille doit être 9, valeur reçue : {taille}");
        this.taille = taille;
        this.console = console;
        this.chargeur = chargeur;
    }

    public Case GetCase(int ligne, int colonne)
    {
        if (cases == null)
            throw new EGrilleCharge("La grille n'est pas chargée");
        if (ligne < 0 || ligne >= taille || colonne < 0 || colonne >= taille)
            throw new EGrilleCoordonnees($"Coordonnées invalides : ligne={ligne}, colonne={colonne}");
        return cases[ligne, colonne];
    }

    public void Charger()
    {
        cases = chargeur.ChargerGrille(this);
    }

    public void Afficher()
    {
        if (cases == null)
            throw new EGrilleCharge("La grille n'est pas chargée");
        console.AfficherGrille(this);
    }
}
