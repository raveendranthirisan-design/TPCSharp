public class Cercle : Forme
{
    public double Rayon { get; }

    public Cercle(double rayon) : base("Cercle")
    {
        if (rayon <= 0)
        {
            throw new ArgumentOutOfRangeException("rayon");
        }
        Rayon = rayon;
    }

    public override double Aire()
    {
        return Math.PI * Rayon * Rayon;
    }

    public override double Perimetre()
    {
        return 2 * Math.PI * Rayon;
    }
}
