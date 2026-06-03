using Creator.Model;
using Metier.Model;
using System.Windows;

namespace TranspireWpf;

public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        int difficulte = 3;

        MainWindow mainWindow = new MainWindow(difficulte);
        Current.MainWindow = mainWindow;
        mainWindow.Show();

        DifficulteWindow difficulteWindow = new DifficulteWindow { Owner = mainWindow };
        bool? result = difficulteWindow.ShowDialog();

        if (result == true)
        {
            difficulte = difficulteWindow.Difficulte;
            mainWindow.ChargerGrille(difficulte);
        }
    }
}