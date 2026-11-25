// Un etudiant de la promo, avec la liste de ses notes.
public class Etudiant
{
    public int Id { get; set; } // clé primaire
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public required string Promo { get; set; }
    public string? Email { get; set; } // peut être null
    public List<Note> Notes { get; set; } = []; // navigation : 1 → n
}


// Une note d'un etudiant dans une matiere.
public class Note
{
    public int Id { get; set; }
    public required string Matiere { get; set; }
    public double Valeur { get; set; }
    public int Coef { get; set; }
    public int EtudiantId { get; set; } // clé étrangère
    public Etudiant Etudiant { get; set; } = null!; // navigation : n → 1
}
