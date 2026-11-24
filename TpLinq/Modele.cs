// Une note d'un etudiant dans une matiere.
public record Note(string Nom, string Prenom, string Matiere, double Valeur, int Coef);

// Un etudiant inscrit dans une promo.
public record Inscrit(string Nom, string Prenom, string Promo);

// Le bulletin d'un etudiant : son prenom et la liste de ses valeurs.
public record Bulletin(string Prenom, List<double> Valeurs);

// Les methodes de la partie delegues et lambdas.
public static class Modele
{
    // Garde les notes qui verifient le critere (comme Where, mais avec une boucle).
    public static List<Note> Filtrer(List<Note> source, Func<Note, bool> critere)
    {
        List<Note> notes = new List<Note>();

        foreach (Note n in source)
        {
            if (critere(n))
            {
                notes.Add(n);
            }
        }

        return notes;
    }

    // Vrai si la note est en Algo.
    public static bool EstEnAlgo(Note n)
    {
        return n.Matiere == "Algo";
    }

    // Renvoie une fonction qui calcule valeur x coef.
    public static Func<Note, double> Ponderation()
    {
        return n => n.Valeur * n.Coef;
    }

    // Renvoie une Action qui affiche une note : Ada · Algo · 15
    public static Action<Note> AffichageNote()
    {
        return n => Console.WriteLine($"{n.Prenom} · {n.Matiere} · {n.Valeur}");
    }

    // Renvoie un critere : la note doit etre >= au seuil.
    public static Func<Note, bool> AuDessusDe(double seuil)
    {
        return n => n.Valeur >= seuil;
    }
}
