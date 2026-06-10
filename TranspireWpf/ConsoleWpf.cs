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

    public ConsoleWpf(UniformGrid panel, TextBlock? modeLabel = null)
    {
        this.panel = panel;
        this.modeLabel = modeLabel;
    }

    public void AfficherGrille(Grille grille)
    {
        int t = grille.Taille;
        int racine = (int)Math.Sqrt(t);
        panel.Children.Clear();

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
                    Child = CreerContenu(cas)
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

    /// <summary>
    /// Crée le contenu d'une case : valeur centrée si remplie, sinon mini-grille de choix
    /// </summary>
    private UIElement CreerContenu(Case cas)
    {
        if (cas.Affiche)
        {
            Brush couleur = Brushes.Red;
            if (cas.Initiale)
                couleur = Brushes.Blue;

            return new TextBlock
            {
                Text = cas.Valeur.ToString(),
                Foreground = couleur,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 28,
                FontWeight = FontWeights.Bold
            };
        }

        UniformGrid mini = new UniformGrid { Rows = 3, Columns = 3 };
        for (int chiffre = 1; chiffre <= 9; chiffre++)
        {
            string texte = "";
            if (cas.Choix.Contains(chiffre))
                texte = chiffre.ToString();

            TextBlock tb = new TextBlock
            {
                Text = texte,
                Foreground = Brushes.Green,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 11
            };
            mini.Children.Add(tb);
        }
        return mini;
    }
}
