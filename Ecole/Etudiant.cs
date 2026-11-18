namespace Ecole;

// Un etudiant avec son nom, son prenom, son annee et ses notes.
public class Etudiant
{
    // les notes sont privees pour que personne ne puisse les changer sans passer par AjouterNote
    private List<double> _notes = new List<double>();
    private int annee = 1;

    // Le nom (lecture seule, fixe par le constructeur).
    public string Nom { get; }
    // Le prenom (lecture seule, fixe par le constructeur).
    public string Prenom { get; }
    // La date de naissance (init : on peut la mettre seulement a la creation).
    public DateOnly Naissance { get; init; }

    // L'annee d'etudes, entre 1 et 5.
    public int Annee
    {
        get { return annee; }
        set
        {
            if (value < 1 || value > 5)
            {
                throw new ArgumentOutOfRangeException("value");
            }
            annee = value;
        }
    }

    // Les notes en lecture seule (on ne peut pas les modifier de l'exterieur).
    public IReadOnlyList<double> Notes
    {
        get { return _notes; }
    }

    // Le prenom puis le nom en majuscules, par exemple "Ada LOVELACE".
    public string NomComplet
    {
        get { return Prenom + " " + Nom.ToUpper(); }
    }

    // Le nombre de notes de l'etudiant.
    public int NbNotes
    {
        get { return _notes.Count; }
    }

    // Cree un etudiant.
    public Etudiant(string nom, string prenom, int annee)
    {
        if (string.IsNullOrWhiteSpace(nom))
        {
            throw new ArgumentException("Le nom ne peut pas être vide.");
        }
        if (string.IsNullOrWhiteSpace(prenom))
        {
            throw new ArgumentException("Le prénom ne peut pas être vide.");
        }
        Nom = nom;
        Prenom = prenom;
        Annee = annee;
    }

    // Ajoute une note a l'etudiant.
    public void AjouterNote(double n)
    {
        if (n < 0 || n > 20)
        {
            throw new ArgumentOutOfRangeException("n");
        }
        _notes.Add(n);
    }

    // Calcule la moyenne des notes (0 s'il n'y en a pas).
    public double Moyenne()
    {
        if (_notes.Count == 0)
        {
            return 0;
        }

        double somme = 0;
        foreach (double note in _notes)
        {
            somme += note;
        }
        return somme / _notes.Count;
    }

    // Texte de l'etudiant, par exemple "Ada LOVELACE (M1) : 15,50". Annees 1 a 3 = L1 a L3, 4 et 5 = M1 et M2.
    public override string ToString()
    {
        string niveau = Annee switch
        {
            1 => "L1",
            2 => "L2",
            3 => "L3",
            4 => "M1",
            _ => "M2"
        };
        return NomComplet + " (" + niveau + ") : " + Moyenne().ToString("F2");
    }
}
