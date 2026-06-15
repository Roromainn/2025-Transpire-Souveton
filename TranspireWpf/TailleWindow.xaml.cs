using System.Windows;

namespace TranspireWpf;

public partial class TailleWindow : Window
{
    private int taille;
    public int Taille 
    {
        get => taille;
        set => taille = value;
    }

    public TailleWindow()
    {
        InitializeComponent();
    }

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button button && int.TryParse(button.Content.ToString(), out int t))
        {
            this.taille = t;
            DialogResult = true;
            Close();
        }
    }
}
