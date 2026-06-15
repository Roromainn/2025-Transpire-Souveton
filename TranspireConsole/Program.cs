using Creator.Model;
using Metier.Model;
using TranspireConsole;
using System;

Console.WriteLine("Choisir taille (4 ou 9): ");
int taille = 9;
bool tailleValide = false;

while (!tailleValide)
{
    string inputTaille = Console.ReadLine();
    if (int.TryParse(inputTaille, out int t) && (t == 4 || t == 9))
    {
        taille = t;
        tailleValide = true;
    }
    else
    {
        Console.WriteLine("Taille invalide. Entrez 4 ou 9: ");
    }
}

// Demander la difficulté
Console.WriteLine("Choisir difficulté (1-6): ");
int difficulte = 3;
bool difficulteValide = false;

while (!difficulteValide)
{
    string input = Console.ReadLine();
    if (int.TryParse(input, out int d) && d >= 1 && d <= 6)
    {
        difficulte = d;
        difficulteValide = true;
    }
    else
    {
        Console.WriteLine("Difficulté invalide. Entrez un nombre entre 1 et 6: ");
    }
}

ConsoleTexte consoleTexte = new ConsoleTexte();
ChargeurHasard chargeur = new ChargeurHasard(taille, difficulte);
Grille grille = new Grille(taille, consoleTexte, chargeur);
grille.Charger();

bool continuer = true;
while (continuer)
{
    grille.Afficher();
    
    ConsoleKeyInfo touche = Console.ReadKey(true);
    
    try
    {
        switch (touche.Key)
        {
            case ConsoleKey.UpArrow:
                grille.Curseur.Ligne--;
                break;
            case ConsoleKey.DownArrow:
                grille.Curseur.Ligne++;
                break;
            case ConsoleKey.LeftArrow:
                grille.Curseur.Colonne--;
                break;
            case ConsoleKey.RightArrow:
                grille.Curseur.Colonne++;
                break;
            case ConsoleKey.Enter:
                if (grille.Choix)
                {
                    if (grille.ValeurSelectionne.HasValue)
                    {
                        grille.GetCase(grille.Curseur.Ligne, grille.Curseur.Colonne).Choisir(grille.ValeurSelectionne.Value);
                    }
                }
                else
                {
                    grille.EnleverChoix();
                    grille.MettreValeur();
                    if (grille.PartieTerminee)
                        continuer = false;
                }
                break;
            case ConsoleKey.M:
            case ConsoleKey.Spacebar:
                grille.ChangerMode();
                break;
            case ConsoleKey.Q:
                continuer = false;
                break;
            default:
                if (char.IsDigit(touche.KeyChar))
                {
                    int val = int.Parse(touche.KeyChar.ToString());
                    if (val >= 1 && val <= grille.Taille)
                    {
                        grille.ValeurSelectionne = val;
                    }
                }
                else if (char.ToUpper(touche.KeyChar) == 'M')
                {
                    grille.ChangerMode();
                }
                break;
        }
    }
    catch (Exception)
    {
    }
}