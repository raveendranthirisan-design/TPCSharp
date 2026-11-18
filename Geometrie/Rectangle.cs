namespace Geometrie;

// Un rectangle, avec une largeur et une hauteur.
public class Rectangle : Forme
{
    // La largeur (strictement positive).
    public double Largeur { get; }
    // La hauteur (strictement positive).
    public double Hauteur { get; }

    // Cree un rectangle.
    public Rectangle(double largeur, double hauteur) : base("Rectangle")
    {
        if (largeur <= 0)
        {
            throw new ArgumentOutOfRangeException("largeur");
        }
        if (hauteur <= 0)
        {
            throw new ArgumentOutOfRangeException("hauteur");
        }
        Largeur = largeur;
        Hauteur = hauteur;
    }

    // Aire = largeur * hauteur.
    public override double Aire()
    {
        return Largeur * Hauteur;
    }

    // Perimetre = 2 * (largeur + hauteur).
    public override double Perimetre()
    {
        return 2 * (Largeur + Hauteur);
    }
}
