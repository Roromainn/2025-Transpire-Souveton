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
        this.taille = taille;
        DialogResult = true;
        Close();
        
    }
}
