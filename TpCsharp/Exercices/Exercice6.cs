using Ecole;

// Teste le record Note.
public static class Exercice6
{
    // Compare des notes, en affiche une et fait une copie avec with.
    public static void Run()
    {
        Note n1 = new Note("Lovelace", "Ada", "Algo", 15, 3);
        Note n2 = new Note("Lovelace", "Ada", "Algo", 15, 3);

        Console.WriteLine("n1 == n2 : " + (n1 == n2));
        Console.WriteLine(n1);

        // copie de n1 avec une autre valeur
        Note n3 = n1 with { Valeur = 18 };
        Console.WriteLine(n3);
        Console.WriteLine("n1 == n3 : " + (n1 == n3));

        // pour une classe, == compare les references et pas les donnees
        Etudiant e1 = new Etudiant("Lovelace", "Ada", 4);
        Etudiant e2 = new Etudiant("Lovelace", "Ada", 4);
        Console.WriteLine("e1 == e2 : " + (e1 == e2));

        List<Note> notes;
        try
        {
            notes = LireFichier("notes.csv");
        }
        catch (FileNotFoundException)
        {
            Console.Error.WriteLine("Le fichier notes.csv est introuvable. Il doit etre a la racine du projet.");
            return;
        }

        // on cree un etudiant par nom + prenom, puis on lui ajoute ses notes valides
        // (le fichier ne donne pas l'annee donc on met 4 pour tout le monde)
        Dictionary<string, Etudiant> etudiants = new Dictionary<string, Etudiant>();
        Promo promo = new Promo("M1 Info");

        foreach (Note note in notes)
        {
            string cle = note.Prenom + " " + note.Nom;
            if (!etudiants.TryGetValue(cle, out Etudiant? etudiant))
            {
                etudiant = new Etudiant(note.Nom, note.Prenom, 4);
                etudiants[cle] = etudiant;
                promo.Inscrire(etudiant);
            }
            etudiant.AjouterNote(note.Valeur);
        }

        promo.AfficherReleve();
    }

    // Lit le fichier ligne par ligne. Les lignes fausses sont signalees sur Console.Error puis ignorees.
    public static List<Note> LireFichier(string chemin)
    {
        List<Note> notes = new List<Note>();

        using StreamReader lecteur = new StreamReader(chemin);

        // on saute l'en-tete (ligne 1)
        lecteur.ReadLine();
        int numero = 1;

        string? ligne;
        while ((ligne = lecteur.ReadLine()) != null)
        {
            numero++;
            try
            {
                notes.Add(Note.LireLigne(ligne, numero));
            }
            catch (NoteInvalideException e)
            {
                Console.Error.WriteLine("Ligne " + e.Ligne + " invalide : " + ligne);
            }
        }

        return notes;
    }
}
