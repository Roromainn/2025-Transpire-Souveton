using Metier.Interfaces;
using Metier.Model;

namespace TranspireConsole;

public class ConsoleTexte : IConsole
{
    private string LigneSep(int t, char gauche, char milieu, char droite)
    {
        string ligne = "" + gauche;
        for (int i = 0; i < t; i++)
        {
            ligne += "\u2500\u2500\u2500";
            if (i < t - 1) ligne += milieu;
        }
        return ligne + droite;
    }

    public void AfficherGrille(Grille grille)
    {
        int t = grille.Taille;

        Console.WriteLine(LigneSep(t, '\u250C', '\u252C', '\u2510'));

        for (int l = 0; l < t; l++)
        {
            for (int c = 0; c < t; c++)
            {
                var cas = grille.GetCase(l, c);
                string val = " ";
                if (cas.Affiche) val = cas.Valeur.ToString();
                Console.Write("\u2502 " + val + " ");
            }
            Console.WriteLine("\u2502");

            if (l < t - 1)
                Console.WriteLine(LigneSep(t, '\u251C', '\u253C', '\u2524'));
        }

        Console.WriteLine(LigneSep(t, '\u2514', '\u2534', '\u2518'));
    }
}
