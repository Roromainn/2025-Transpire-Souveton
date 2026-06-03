using Creator.Model;
using Metier.Model;
using TranspireConsole;
using System;

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
ChargeurHasard chargeur = new ChargeurHasard(9, difficulte);
Grille grille = new Grille(9, consoleTexte, chargeur);
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
                grille.MettreValeur();
                break;
            case ConsoleKey.Q:
                continuer = false;
                break;
            default:
                if (char.IsDigit(touche.KeyChar))
                {
                    int val = int.Parse(touche.KeyChar.ToString());
                    if (val >= 1 && val <= 9)
                    {
                        grille.ValeurSelectionne = val;
                    }
                }
                break;
        }
    }
    catch (Exception)
    {
    }
}