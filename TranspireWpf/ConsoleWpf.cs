using Metier.Interfaces;
using Metier.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace TranspireWpf;

public class ConsoleWpf : IConsole
{
    private readonly UniformGrid panel;
    private readonly TextBlock? modeLabel;
    private readonly TextBlock? erreursLabel;

    public ConsoleWpf(UniformGrid panel, TextBlock? modeLabel = null, TextBlock? erreursLabel = null)
    {
        this.panel = panel;
        this.modeLabel = modeLabel;
        this.erreursLabel = erreursLabel;
    }

    public void AfficherGrille(Grille grille)
    {
        int t = grille.Taille;
        int racine = (int)Math.Sqrt(t);
        panel.Children.Clear();
        if (erreursLabel != null)
            erreursLabel.Text = "Erreurs : " + grille.Erreurs + " / 3";

        if (modeLabel != null)
        {
            if (grille.Choix)
            {
                modeLabel.Text = "Mode: CHOIX";
                modeLabel.Foreground = Brushes.Green;
            }
            else
            {
                modeLabel.Text = "Mode: TEST";
                modeLabel.Foreground = Brushes.DarkOrange;
            }
        }

        int Sr = grille.Curseur.Ligne;
        int Sc = grille.Curseur.Colonne;

        for (int l = 0; l < t; l++)
        {
            for (int c = 0; c < t; c++)
            {
                Case cas = grille.GetCase(l, c);
                bool estCurseur = (l == Sr && c == Sc);

                double gauche = 0.5;
                if (c % racine == 0)
                    gauche = 2;

                double haut = 0.5;
                if (l % racine == 0)
                    haut = 2;

                double droite = 0;
                if (c == t - 1)
                    droite = 2;

                double bas = 0;
                if (l == t - 1)
                    bas = 2;

                Brush couleurBordure = Brushes.Black;
                Thickness epaisseurBordure = new Thickness(gauche, haut, droite, bas);
                if (estCurseur)
                {
                    couleurBordure = Brushes.Red;
                    epaisseurBordure = new Thickness(2);
                }

                Border border = new Border
                {
                    BorderBrush = couleurBordure,
                    BorderThickness = epaisseurBordure,
                    Background = Brushes.Transparent,
                    Child = CreerContenu(cas, t, racine)
                };

                int ligne = l;
                int colonne = c;
                border.MouseDown += (s, e) =>
                {
                    try
                    {
                        grille.Curseur = new Coordonnes(t) { Ligne = ligne, Colonne = colonne };

                        if (grille.Choix)
                        {
                            if (grille.ValeurSelectionne.HasValue)
                                grille.GetCase(ligne, colonne).Choisir(grille.ValeurSelectionne.Value);
                        }
                        else
                        {
                            if (grille.ValeurSelectionne.HasValue)
                            {
                                grille.EnleverChoix();
                                grille.MettreValeur();
                            }
                        }

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

    public void AfficherFin(string message)
    {
        System.Windows.MessageBox.Show(message, "Fin de partie", System.Windows.MessageBoxButton.OK);
    }

    /// <summary>
    /// Convertit une valeur en symbole : 1-9 en chiffres, 10-16 en lettres A-G
    /// </summary>
    public static string Symbole(int valeur)
    {
        if (valeur <= 9)
            return valeur.ToString();
        return ((char)('A' + valeur - 10)).ToString();
    }

    /// <summary>
    /// Taille de police de la valeur remplie selon la taille de la grille
    /// </summary>
    private double PoliceValeur(int t)
    {
        if (t == 4)
            return 40;
        if (t == 9)
            return 28;
        return 16;
    }

    /// <summary>
    /// Crée le contenu d'une case : valeur centrée si remplie, sinon mini-grille de choix
    /// </summary>
    private UIElement CreerContenu(Case cas, int t, int racine)
    {
        if (cas.Affiche)
        {
            Brush couleur = Brushes.Red;
            if (cas.Initiale)
                couleur = Brushes.Blue;

            return new TextBlock
            {
                Text = Symbole(cas.Valeur),
                Foreground = couleur,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = PoliceValeur(t),
                FontWeight = FontWeights.Bold
            };
        }

        double policeMini = PoliceValeur(t) / racine;
        UniformGrid mini = new UniformGrid { Rows = racine, Columns = racine };
        for (int chiffre = 1; chiffre <= t; chiffre++)
        {
            string texte = "";
            if (cas.Choix.Contains(chiffre))
                texte = Symbole(chiffre);

            TextBlock tb = new TextBlock
            {
                Text = texte,
                Foreground = Brushes.Green,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = policeMini
            };
            mini.Children.Add(tb);
        }
        return mini;
    }
}
