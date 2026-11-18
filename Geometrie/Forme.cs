namespace Geometrie;

// Classe de base de toutes les formes. Elle est abstraite donc on ne peut pas faire new Forme(). On peut comparer deux formes avec leur aire.
public abstract class Forme : IDessinable, IComparable<Forme>
{
    // Le nom de la forme (lecture seule).
    public string Nom { get; }

    // Constructeur protected : seules les classes filles peuvent l'appeler.
    protected Forme(string nom)
    {
        Nom = nom;
    }

    // Calcule l'aire de la forme.
    public abstract double Aire();
    // Calcule le perimetre de la forme.
    public abstract double Perimetre();

    // Renvoie le nom, l'aire et le perimetre avec deux decimales. Elle est virtual pour que les classes filles puissent la changer.
    public virtual string Decrire()
    {
        return Nom + " : aire = " + Aire().ToString("F2") + ", périmètre = " + Perimetre().ToString("F2");
    }

    // Compare cette forme a une autre avec leurs aires (pour le Sort).
    public int CompareTo(Forme? autre)
    {
        if (autre == null)
        {
            return 1;
        }
        return Aire().CompareTo(autre.Aire());
    }

    // Affiche la description de la forme.
    public void Dessiner()
    {
        Console.WriteLine(Decrire());
    }
}
