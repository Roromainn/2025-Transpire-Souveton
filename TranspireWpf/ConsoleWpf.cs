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
                var cas = grille.GetCase(l, c);

                var border = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    Width = 50,
                    Height = 50
                };

                var text = new TextBlock
                {
                    Text = cas.Valeur.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18
                };

                border.Child = text;
                panel.Children.Add(border);
            }
        }
    }
}