using System.Windows;

namespace TranspireWpf;

public partial class DifficulteWindow : Window
{
    public int Difficulte { get; private set; }

    public DifficulteWindow()
    {
        InitializeComponent();
    }

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button button && int.TryParse(button.Content.ToString(), out int difficulte))
        {
            Difficulte = difficulte;
            DialogResult = true;
            Close();
        }
    }
}
