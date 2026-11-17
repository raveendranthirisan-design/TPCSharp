namespace Geometrie;

public abstract class Forme : IDessinable, IComparable<Forme>
{
    public string Nom { get; }

    protected Forme(string nom)
    {
        Nom = nom;
    }

    public abstract double Aire();
    public abstract double Perimetre();

    public virtual string Decrire()
    {
        return Nom + " : aire = " + Aire().ToString("F2") + ", périmètre = " + Perimetre().ToString("F2");
    }

    public int CompareTo(Forme? autre)
    {
        if (autre == null)
        {
            return 1;
        }
        return Aire().CompareTo(autre.Aire());
    }

    public void Dessiner()
    {
        Console.WriteLine(Decrire());
    }
}
