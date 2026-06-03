using Creator.Model;
using Metier.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TranspireWpf;

public partial class MainWindow : Window
{
    private Grille grille;

    public MainWindow(int difficulte)
    {
        InitializeComponent();

        ChargeurHasard chargeur = new ChargeurHasard(9, difficulte);
        ConsoleWpf console = new ConsoleWpf(this.GrillePanel);
        grille = new Grille(9, console, chargeur);

        grille.Charger();
        grille.Afficher();

        CreerBoutons();
    }

    private void CreerBoutons()
    {
        for (int i = 1; i <= 9; i++)
        {
            int val = i;
            Button btn = new Button
            {
                Content = val.ToString(),
                Width = 30,
                Height = 30,
                Margin = new Thickness(5),
                Background = Brushes.White
            };
            btn.Click += (s, e) => {
                grille.ValeurSelectionne = val;
                MettreAJourBoutons();
            };
            BoutonsPanel.Children.Add(btn);
        }
    }

    private void MettreAJourBoutons()
    {
        foreach (Button btn in BoutonsPanel.Children)
        {
            if (btn.Content.ToString() == grille.ValeurSelectionne?.ToString())
            {
                btn.Foreground = Brushes.Red;
                btn.BorderBrush = Brushes.Red;
            }
            else
            {
                btn.Foreground = Brushes.Black;
                btn.ClearValue(Button.BorderBrushProperty);
            }
        }
    }

    public void ChargerGrille(int difficulte)
    {
        ChargeurHasard chargeur = new ChargeurHasard(9, difficulte);
        ConsoleWpf console = new ConsoleWpf(this.GrillePanel);
        grille = new Grille(9, console, chargeur);
        grille.Charger();
        grille.Afficher();
    }
}