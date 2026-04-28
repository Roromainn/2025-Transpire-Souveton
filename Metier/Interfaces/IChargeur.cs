using Metier.Model;

namespace Metier.Interfaces;

public interface IChargeur
{
    Case[,] ChargerGrille(Grille grille);
}
