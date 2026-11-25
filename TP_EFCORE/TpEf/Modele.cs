public class Etudiant
{
public int Id { get; set; } // clé primaire
public required string Nom { get; set; }
public required string Prenom { get; set; }
public required string Promo { get; set; }
public List<Note> Notes { get; set; } = []; // navigation : 1 → n
}


public class Note
{
public int Id { get; set; }
public required string Matiere { get; set; }
public double Valeur { get; set; }
public int Coef { get; set; }
public int EtudiantId { get; set; } // clé étrangère
public Etudiant Etudiant { get; set; } = null!; // navigation : n → 1
}