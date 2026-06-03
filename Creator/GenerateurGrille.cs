using Metier.Model;

namespace Creator.Model;

/// <summary>
/// Classe pour générer des grilles Sudoku
/// </summary>
public class GenerateurGrille
{
    private int taille;
    private int racine; // racine carrée de taille 

    public GenerateurGrille(int taille)
    {
        this.taille = taille;
        this.racine = (int)Math.Sqrt(taille);
    }

    /// <summary>
    /// Remplit la grille avec des 0
    /// </summary>
    public int[,] GrilleVide()
    {
        return new int[taille, taille];
    }

    /// <summary>
    /// Remplit la grille à partir de la position donnée 
    /// </summary>
    public bool RemplirGrille(int[,] grille, int ligne, int colonne)
    {
        //Trouver la prochaine case vide
        while (ligne < taille)
        {
            while (colonne < taille && grille[ligne, colonne] != 0)
            {
                colonne++;
            }

            if (colonne == taille)
            {
                ligne++;
                colonne = 0;
                continue;
            }

            //essayer les valeurs possibles
            var valeursPossibles = CalculerValeurPossibles(grille, ligne, colonne);
            foreach (var valeur in valeursPossibles)
            {
                grille[ligne, colonne] = valeur;
                if (RemplirGrille(grille, ligne, colonne + 1))
                    return true;
                grille[ligne, colonne] = 0;
            }

            return false;
        }

        return true;
    }

    /// <summary>
    /// Calcule les valeurs possibles pour une position donnée
    /// </summary>
    public List<int> CalculerValeurPossibles(int[,] grille, int ligne, int colonne)
    {
        var possibles = new HashSet<int>();
        for (int i = 1; i <= taille; i++)
        {
            possibles.Add(i);
        }

        // Enlever les valeurs de la ligne
        for (int c = 0; c < taille; c++)
        {
            if (grille[ligne, c] != 0)
                possibles.Remove(grille[ligne, c]);
        }

        // Enlever les valeurs de la colonne
        for (int l = 0; l < taille; l++)
        {
            if (grille[l, colonne] != 0)
                possibles.Remove(grille[l, colonne]);
        }

        // Enlever les valeurs de la sous-grille
        int sousLigneDebut = (ligne / racine) * racine;
        int sousColonneDebut = (colonne / racine) * racine;
        for (int l = sousLigneDebut; l < sousLigneDebut + racine; l++)
        {
            for (int c = sousColonneDebut; c < sousColonneDebut + racine; c++)
            {
                if (grille[l, c] != 0)
                    possibles.Remove(grille[l, c]);
            }
        }

        return possibles.ToList();
    }

    /// <summary>
    /// Calcule le nombre de solutions possibles à partir d'une position
    /// </summary>
    public int CalculerNombreSolutions(int[,] grille, int ligne, int colonne)
    {
        var gridCopy = (int[,])grille.Clone();
        return CompterSolutions(gridCopy, ligne, colonne);
    }

    private int CompterSolutions(int[,] grille, int ligne, int colonne)
    {
        // Trouver la prochaine case vide
        while (ligne < taille)
        {
            while (colonne < taille && grille[ligne, colonne] != 0)
            {
                colonne++;
            }

            if (colonne == taille)
            {
                ligne++;
                colonne = 0;
                continue;
            }

            // Compter les solutions
            int count = 0;
            var valeursPossibles = CalculerValeurPossibles(grille, ligne, colonne);

            foreach (var valeur in valeursPossibles)
            {
                grille[ligne, colonne] = valeur;
                count += CompterSolutions(grille, ligne, colonne + 1);
                grille[ligne, colonne] = 0;
            }

            return count;
        }

        return 1; // 1 seule solution
    }

    /// <summary>
    /// Retourne la case suivante, ou null si c'est la dernière case
    /// </summary>
    public (int ligne, int colonne)? CaseSuivante(int ligne, int colonne)
    {
        colonne++;
        if (colonne == taille)
        {
            colonne = 0;
            ligne++;
        }

        if (ligne == taille)
            return null;

        return (ligne, colonne);
    }
}
