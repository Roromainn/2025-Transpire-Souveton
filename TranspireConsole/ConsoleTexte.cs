using Metier.Interfaces;
using Metier.Model;
using System;

namespace TranspireConsole;

public class ConsoleTexte : IConsole
{
    private void ColorIf(bool condition)
    {
        if (condition) 
            Console.ForegroundColor = ConsoleColor.Red;
        else 
            Console.ResetColor();
    }

    private void DessinerLigneSep(Grille grille, int lSep)
    {
        int t = grille.Taille;
        int Sr = grille.Curseur.Ligne;
        int Sc = grille.Curseur.Colonne;

        char gauche, milieu, droite;
        if (lSep == -1) 
        {
            gauche = '\u250C'; 
            milieu = '\u252C'; 
            droite = '\u2510'; 
        }
        else if (lSep == t - 1) 
        {
            gauche = '\u2514'; 
            milieu = '\u2534'; 
            droite = '\u2518'; 
        }
        else 
        {
            gauche = '\u251C';
            milieu = '\u253C';
            droite = '\u2524';
        }

        ColorIf((lSep == Sr - 1 || lSep == Sr) && Sc == 0);
        Console.Write(gauche);
        Console.ResetColor();

        for (int c = 0; c < t; c++)
        {
            ColorIf((lSep == Sr - 1 || lSep == Sr) && Sc == c);
            Console.Write("\u2500\u2500\u2500");
            Console.ResetColor();

            if (c < t - 1)
            {
                ColorIf((lSep == Sr - 1 || lSep == Sr) && (Sc == c || Sc == c + 1));
                Console.Write(milieu);
                Console.ResetColor();
            }
        }

        ColorIf((lSep == Sr - 1 || lSep == Sr) && Sc == t - 1);
        Console.Write(droite);
        Console.ResetColor();
        Console.WriteLine();
    }

    public void AfficherGrille(Grille grille)
    {
        Console.Clear();
        int t = grille.Taille;
        int Sr = grille.Curseur.Ligne;
        int Sc = grille.Curseur.Colonne;

        for (int l = 0; l < t; l++)
        {
            DessinerLigneSep(grille, l - 1);

            for (int c = 0; c < t; c++)
            {
                ColorIf(l == Sr && (Sc == c || (c > 0 && Sc == c - 1)));
                Console.Write("\u2502");
                Console.ResetColor();

                Case cas = grille.GetCase(l, c);
                if (cas.Initiale) 
                    Console.ForegroundColor = ConsoleColor.Blue;
                else 
                    Console.ForegroundColor = ConsoleColor.White;

                string val = cas.Affiche ? cas.Valeur.ToString() : " ";
                Console.Write(" " + val + " ");
                Console.ResetColor();
            }

            ColorIf(l == Sr && Sc == t - 1);
            Console.WriteLine("\u2502");
            Console.ResetColor();
        }

        DessinerLigneSep(grille, t - 1);

        Console.Write("\nValeur sélectionnée : ");
        for (int i = 1; i <= 9; i++)
        {
            if 
                (grille.ValeurSelectionne == i) Console.ForegroundColor = ConsoleColor.Red;
            else 
                Console.ForegroundColor = ConsoleColor.White;
            Console.Write(i + " ");
            Console.ResetColor();
        }
    }
}