using Metier.Model;
using System.Windows;

namespace TranspireWpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        var chargeur = new ChargeurDefaut(9);
        var console = new ConsoleWpf(this.GrillePanel);
        var grille = new Grille(9, console, chargeur);
        
        grille.Charger();
        grille.Afficher();
    }
}