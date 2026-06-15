using Metier.Interfaces;
using Metier.Model;
using System;
using System.Linq;

namespace TranspireConsole;

public class ConsoleTexte : IConsole
{
    public void AfficherGrille(Grille grille)
    {
        Console.Clear();
        int t = grille.Taille;
        int Sr = grille.Curseur.Ligne;
        int Sc = grille.Curseur.Colonne;

        Console.ForegroundColor = grille.Choix ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine($"Mode: {(grille.Choix ? "CHOIX" : "TEST")} (Appuyez sur M pour changer)");
        Console.ResetColor();
        Console.WriteLine();
        DessinerSeparateur(t, -1, 0, Sr, Sc);

        for (int l = 0; l < t; l++)
        {
            for (int miniLine = 0; miniLine < 3; miniLine++)
            {
                for (int c = 0; c < t; c++)
                {
                    bool bordGaucheRouge = (l == Sr) && (c == Sc || c == Sc + 1);
                    EcrireBordureV(bordGaucheRouge, c, t);
                    Case cas = grille.GetCase(l, c);
                    for (int miniCol = 0; miniCol < 3; miniCol++)
                    {
                        if (cas.Affiche)
                        {
                            if (miniLine == 1 && miniCol == 1)
                            {
                                Console.ForegroundColor = cas.Initiale ? ConsoleColor.Blue : ConsoleColor.Red;
                                Console.Write(cas.Valeur);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            int chiffre = miniLine * 3 + miniCol + 1;
                            if (cas.Choix.Contains(chiffre))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(chiffre);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }

                        if (miniCol < 2)
                            Console.Write(" ");
                    }
                }
                bool bordDroiteRouge = (l == Sr) && (Sc == t - 1);
                EcrireBordureV(bordDroiteRouge, t, t);
                Console.WriteLine();
            }
            DessinerSeparateur(t, l, l + 1, Sr, Sc);
        }

        Console.Write("\nValeur selectionnee : ");
        for (int i = 1; i <= t; i++)
        {
            if (grille.ValeurSelectionne == i)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.Write(i + " ");
            Console.ResetColor();
        }
        Console.WriteLine("\nErreurs : " + grille.Erreurs + "/3");
    }

    public void AfficherFin(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n\n");
        Console.WriteLine("  ╔══════════════════════════════════════╗");
        Console.WriteLine("  ║                                      ║");
        string ligne = "  ║  " + message;
        ligne = ligne.PadRight(41) + "║";
        Console.WriteLine(ligne);
        Console.WriteLine("  ║                                      ║");
        Console.WriteLine("  ╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine("\n  Appuyez sur une touche pour quitter...");
        Console.ReadKey(true);
    }

    /// <summary>
    /// Écrit une bordure verticale, en rouge si curseur
    /// </summary>
    private void EcrireBordureV(bool rouge, int c, int t)
    {
        int racine = (int)Math.Sqrt(t);
        if (rouge)
            Console.ForegroundColor = ConsoleColor.Red;

        if (c == 0 || c == t)
            Console.Write("│");
        else if (c % racine == 0)
            Console.Write("║");
        else
            Console.Write("│");
        Console.ResetColor();
    }

    /// <summary>
    /// Dessine un séparateur horizontal entre rowAbove et rowBelow.
    /// rowAbove = -1 : bordure du haut ; rowBelow = t : bordure du bas.
    /// </summary>
    private void DessinerSeparateur(int t, int rowAbove, int rowBelow, int Sr, int Sc)
    {
        bool toucheCurseur = (rowAbove == Sr || rowBelow == Sr);

        int racine = (int)Math.Sqrt(t);
        for (int c = 0; c < t; c++)
        {
            bool jonctionRouge = toucheCurseur && (c == Sc || c == Sc + 1);
            EcrireJonction(jonctionRouge, t, rowAbove, rowBelow, c);
            bool segmentRouge = toucheCurseur && c == Sc;
            if (segmentRouge)
                Console.ForegroundColor = ConsoleColor.Red;
            bool ligneDouble = (rowBelow % racine == 0);
            Console.Write(ligneDouble ? "═════" : "─────");
            Console.ResetColor();
        }
        bool jonctionFinRouge = toucheCurseur && (Sc == t - 1);
        EcrireJonction(jonctionFinRouge, t, rowAbove, rowBelow, t);
        Console.WriteLine();
    }

    /// <summary>
    /// Écrit un caractère de jonction adapté à la position
    /// </summary>
    private void EcrireJonction(bool rouge, int t, int rowAbove, int rowBelow, int c)
    {
        if (rouge)
            Console.ForegroundColor = ConsoleColor.Red;

        int racine = (int)Math.Sqrt(t);
        bool ligneDouble = (rowBelow % racine == 0);
        bool colDouble = (c % racine == 0);
        char ch;

        if (rowAbove == -1)
        {
            if (c == 0) ch = '┌';
            else if (c == t) ch = '┐';
            else if (colDouble) ch = '╤';
            else ch = '┬';
        }
        else if (rowBelow == t)
        {
            if (c == 0) ch = '└';
            else if (c == t) ch = '┘';
            else if (colDouble) ch = '╧';
            else ch = '┴';
        }
        else
        {
            if (c == 0) ch = ligneDouble ? '╞' : '├';
            else if (c == t) ch = ligneDouble ? '╡' : '┤';
            else if (ligneDouble && colDouble) ch = '╪';
            else if (ligneDouble) ch = '╪';
            else if (colDouble) ch = '╫';
            else ch = '┼';
        }

        Console.Write(ch);
        Console.ResetColor();
    }
}
