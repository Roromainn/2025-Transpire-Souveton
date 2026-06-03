using Metier.Interfaces;
using Metier.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace TranspireWpf;

public class ConsoleWpf : IConsole
{
    private readonly UniformGrid panel;

    public ConsoleWpf(UniformGrid panel)
    {
        this.panel = panel;
    }

    public void AfficherGrille(Grille grille)
    {
        int t = grille.Taille;
        panel.Children.Clear();

        for (int l = 0; l < t; l++)
        {
            for (int c = 0; c < t; c++)
            {
                Case cas = grille.GetCase(l, c);

                Border border = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    Background = Brushes.Transparent
                };

                TextBlock text = new TextBlock
                {
                    Text = cas.Affiche ? cas.Valeur.ToString() : "",
                    Foreground = cas.Initiale ? Brushes.Blue : Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18
                };

                border.Child = text;

                int ligne = l;
                int colonne = c;
                border.MouseDown += (s, e) =>
                {
                    try
                    {
                        grille.Curseur = new Coordonnes(t) { Ligne = ligne, Colonne = colonne };
                        grille.MettreValeur();
                        grille.Afficher();
                    }
                    catch
                    {
                    }
                };

                panel.Children.Add(border);
            }
        }
    }
}