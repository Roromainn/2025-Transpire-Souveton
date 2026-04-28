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
        this.taille = taille;
        this.console = console;
        this.chargeur = chargeur;
    }

    public Case GetCase(int ligne, int colonne)
    {
        return cases![ligne, colonne];
    }

    public void Charger()
    {
        cases = chargeur.Charger(this);
    }

    public void Afficher()
    {
        console.AfficherGrille(this);
    }
}
