namespace Geometrie;

// Un cercle, avec un rayon.
public class Cercle : Forme
{
    // Le rayon (strictement positif).
    public double Rayon { get; }

    // Cree un cercle.
    public Cercle(double rayon) : base("Cercle")
    {
        if (rayon <= 0)
        {
            throw new ArgumentOutOfRangeException("rayon");
        }
        Rayon = rayon;
    }

    // Aire = pi * r * r.
    public override double Aire()
    {
        return Math.PI * Rayon * Rayon;
    }

    // Perimetre = 2 * pi * r.
    public override double Perimetre()
    {
        return 2 * Math.PI * Rayon;
    }

    // Reprend la description de Forme (avec base) et ajoute le rayon.
    public override string Decrire()
    {
        return base.Decrire() + ", rayon = " + Rayon.ToString("F2");
    }
}
