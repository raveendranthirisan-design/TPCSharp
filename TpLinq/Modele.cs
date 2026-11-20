public record Note(string Nom, string Prenom, string Matiere, double Valeur, int Coef);
public record Inscrit(string Nom, string Prenom, string Promo);


public static class Modele
{
    public static List<Note> Filtrer(List<Note> source, Func<Note, bool> critere)
    {
        List<Note> notes = new List<Note>();

        foreach(Note n in source)
        {
            if (critere(n))
            {
                notes.Add(n);
            }
        }

        return notes;
    }


    public static bool EstEnAlgo(Note n)
    {
        if(n.Matiere == "Algo") return true;
        else return false;
    }


    public static Func<Note, double> Ponderation()
    {
       
        return  n => n.Valeur * n.Coef;
    }

    public static Action<Note> AffichageNote()
    {
        
        return n => Console.WriteLine($"{n.Prenom} · {n.Matiere} · {n.Valeur}");
    }


    public static Func<Note, bool> AuDessusDe(double seuil)
    {
        return n => n.Valeur >= seuil;
    }

    

}