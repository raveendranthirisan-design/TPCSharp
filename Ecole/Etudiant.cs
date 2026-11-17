namespace Ecole;

public class Etudiant
{
    private List<double> _notes = new List<double>();
    private int annee = 1;

    public string Nom { get; }
    public string Prenom { get; }
    public DateOnly Naissance { get; init; }

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

    public IReadOnlyList<double> Notes
    {
        get { return _notes; }
    }

    public string NomComplet
    {
        get { return Prenom + " " + Nom.ToUpper(); }
    }

    public int NbNotes
    {
        get { return _notes.Count; }
    }

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

    public void AjouterNote(double n)
    {
        if (n < 0 || n > 20)
        {
            throw new ArgumentOutOfRangeException("n");
        }
        _notes.Add(n);
    }

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
