using Metier;
using TranspireConsole;

var console = new ConsoleTexte();
var chargeur = new ChargeurDefaut(9);
var grille = new Grille(9, console, chargeur);
grille.Charger();
grille.Afficher();
