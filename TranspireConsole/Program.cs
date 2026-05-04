using Metier.Model;
using TranspireConsole;
using System;

ConsoleTexte consoleTexte = new ConsoleTexte();
ChargeurDefaut chargeur = new ChargeurDefaut(9);
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