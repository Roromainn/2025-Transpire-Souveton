using Creator.Model;
using Metier.Model;
using System.Windows;

namespace TranspireWpf;

public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        int taille = 9;
        int difficulte = 3;

        MainWindow mainWindow = new MainWindow();
        Current.MainWindow = mainWindow;
        mainWindow.Show();

        TailleWindow tailleWindow = new TailleWindow { Owner = mainWindow };
        bool? resultTaille = tailleWindow.ShowDialog();
        if (resultTaille == true)
            taille = tailleWindow.Taille;

        DifficulteWindow difficulteWindow = new DifficulteWindow { Owner = mainWindow };
        bool? result = difficulteWindow.ShowDialog();
        if (result == true)
            difficulte = difficulteWindow.Difficulte;

        mainWindow.ChargerGrille(taille, difficulte);
    }
}