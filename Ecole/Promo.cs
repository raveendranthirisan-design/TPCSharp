namespace Ecole;

// Une promo : un nom et une liste d'etudiants inscrits.
public class Promo
{
    // liste privee, on ajoute seulement avec Inscrire
    private List<Etudiant> etudiants = new List<Etudiant>();

    // Le nom de la promo, par exemple "M1 Info".
    public string Nom { get; }

    // Cree une promo vide.
    public Promo(string nom)
    {
        Nom = nom;
    }

    // Inscrit un etudiant dans la promo.
    public void Inscrire(Etudiant e)
    {
        etudiants.Add(e);
    }

    // Cherche un etudiant avec son nom.
    public Etudiant? Chercher(string nom)
    {
        foreach (Etudiant e in etudiants)
        {
            if (e.Nom == nom)
            {
                return e;
            }
        }
        return null;
    }

    // Trouve l'etudiant avec la meilleure moyenne.
    public Etudiant? Major()
    {
        Etudiant? major = null;

        foreach (Etudiant e in etudiants)
        {
            if (major == null || e.Moyenne() > major.Moyenne())
            {
                major = e;
            }
        }
        return major;
    }

    // Calcule la moyenne des moyennes de tous les etudiants (0 si la promo est vide).
    public double MoyenneGenerale()
    {
        if (etudiants.Count == 0)
        {
            return 0;
        }

        double somme = 0;
        foreach (Etudiant e in etudiants)
        {
            somme += e.Moyenne();
        }
        return somme / etudiants.Count;
    }

    // Affiche le releve : un etudiant par ligne avec sa mention.
    public void AfficherReleve()
    {
        foreach (Etudiant e in etudiants)
        {
            string mention = Program.Mention(e.Moyenne());
            Console.WriteLine(e + " => " + mention);
        }
    }
}
