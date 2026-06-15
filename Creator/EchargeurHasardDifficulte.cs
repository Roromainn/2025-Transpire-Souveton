namespace Creator.ExepCreator;

/// <summary>
/// Exception levée quand difficulté du chargeur hasard invalide
/// </summary>
public class EchargeurHasardDifficulte : Exception
{
    public EchargeurHasardDifficulte(string message) : base(message) { }
}
