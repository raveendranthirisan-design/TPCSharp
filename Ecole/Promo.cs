namespace Ecole;

public class Promo
{
    private List<Etudiant> etudiants = new List<Etudiant>();

    public string Nom { get; }

    public Promo(string nom)
    {
        Nom = nom;
    }

    public void Inscrire(Etudiant e)
    {
        etudiants.Add(e);
    }

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

    public void AfficherReleve()
    {
        foreach (Etudiant e in etudiants)
        {
            string mention = Program.Mention(e.Moyenne());
            Console.WriteLine(e + " => " + mention);
        }
    }
}
