using Creator.Model;
using Metier.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TranspireWpf;

public partial class MainWindow : Window
{
    private Grille grille;

    public MainWindow()
    {
        InitializeComponent();
        ChargerGrille(9, 3);
    }

    private void ModeBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        grille.ChangerMode();
        grille.Afficher();
    }

    private void CreerBoutons()
    {
        BoutonsPanel.Children.Clear();
        int t = grille.Taille;
        BoutonsPanel.Columns = t;
        for (int i = 1; i <= t; i++)
        {
            int val = i;
            Button btn = new Button
            {
                Content = ConsoleWpf.Symbole(val),
                Tag = val,
                Width = 30,
                Height = 30,
                Margin = new Thickness(2),
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
            if (btn.Tag is int val && grille.ValeurSelectionne == val)
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

    public void ChargerGrille(int taille, int difficulte)
    {
        GrillePanel.Rows = taille;
        GrillePanel.Columns = taille;
        ChargeurHasard chargeur = new ChargeurHasard(taille, difficulte);
        ConsoleWpf console = new ConsoleWpf(this.GrillePanel, this.ModeLabel, this.ErreursLabel);
        grille = new Grille(taille, console, chargeur);
        grille.Charger();
        grille.Afficher();
        CreerBoutons();
    }
}
