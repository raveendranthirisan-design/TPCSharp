namespace Geometrie;

public class Rectangle : Forme
{
    public double Largeur { get; }
    public double Hauteur { get; }

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

    public override double Aire()
    {
        return Largeur * Hauteur;
    }

    public override double Perimetre()
    {
        return 2 * (Largeur + Hauteur);
    }
}
