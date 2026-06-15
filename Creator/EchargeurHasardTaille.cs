namespace Creator.ExepCreator;

/// <summary>
/// Exception levée quand taille du chargeur hasard invalide
/// </summary>
public class EchargeurHasardTaille : Exception
{
    public EchargeurHasardTaille(string message) : base(message) { }
}
