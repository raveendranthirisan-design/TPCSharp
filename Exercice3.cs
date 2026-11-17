public static class Exercice3
{
    public static void Run()
    {
        // List<double> notes = [12, 15.5, 9, 18];

        // double somme = 0;
        // double min = notes[0];
        // double max = notes[0];

        // foreach (double note in notes)
        // {
        //     somme += note;
        //     if (note < min) min = note;
        //     if (note > max) max = note;
        // }

        // double moyenne = somme / notes.Count;

        // string mention = Program.Mention(moyenne);
        // Console.WriteLine("Moyenne : " + moyenne.ToString("F2") + " => " + mention);
        // Console.WriteLine($"Note minimale : {min}");
        // Console.WriteLine($"Note maximale : {max}");


        Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();

        dico["Alice"] = new List<double> { 12, 15.5, 9 };
        dico["Bob"] = new List<double> { 8, 11, 7.5 };
        dico["Chloé"] = new List<double> { 17, 18, 16 };

        // on ajoute une note à Bob
        dico["Bob"].Add(14);

        foreach (var (nom, notes) in dico)
        {
            double somme = 0;
            foreach (double note in notes)
            {
                somme += note;
            }

            double moyenne = somme / notes.Count;
            string mention = Program.Mention(moyenne);

            Console.WriteLine(nom + " : " + notes.Count + " notes, moyenne " + moyenne.ToString("F2") + " => " + mention);
        }

        // Question 4 : lire dico["Inconnu"]
        if (dico.TryGetValue("Inconnu", out var notesInconnu))
        {
            Console.WriteLine("Inconnu a " + notesInconnu.Count + " notes");
        }
        else
        {
            Console.WriteLine("Inconnu n'existe pas dans le dictionnaire");
        }

        // Question 5 : on teste Decrire sur six valeurs
        Console.WriteLine(Decrire(4));
        Console.WriteLine(Decrire(7));
        Console.WriteLine(Decrire(""));
        Console.WriteLine(Decrire("bonjour"));
        Console.WriteLine(Decrire(new List<double> { 1, 2, 3 }));
        Console.WriteLine(Decrire(null));
    }

    static string Decrire(object? o)
    {
        return o switch
        {
            int n when n % 2 == 0 => "entier pair",
            int => "entier impair",
            string { Length: 0 } => "chaîne vide",
            string s => "chaîne de " + s.Length + " caractères",
            List<double> liste => "liste de " + liste.Count + " éléments",
            null => "null",
            _ => "autre"
        };
    }
}
