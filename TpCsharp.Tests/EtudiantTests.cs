using Ecole;

namespace TpCsharp.Tests;

// Tests de la classe Etudiant
public class EtudiantTests
{
    // Un etudiant sans note doit avoir 0 de moyenne (pas d'erreur de division par 0)
    [Fact]
    public void MoyenneSansNote()
    {
        // on cree l'etudiant sans lui ajouter de notes
        Etudiant e = new Etudiant("Lovelace", "Ada", 4);

        double moyenne = e.Moyenne();

        Assert.Equal(0, moyenne);
    }

    // Avec plusieurs notes on doit avoir la moyenne normale : (10 + 20) / 2 = 15
    [Fact]
    public void MoyennePlusieursNotes()
    {
        Etudiant e = new Etudiant("Lovelace", "Ada", 4);
        e.AjouterNote(10);
        e.AjouterNote(20);

        double moyenne = e.Moyenne();

        Assert.Equal(15, moyenne);
    }

    // Une note qui n'est pas entre 0 et 20 doit etre refusee
    [Fact]
    public void NoteHorsBornes()
    {
        Etudiant e = new Etudiant("Lovelace", "Ada", 4);

        // 25 est plus grand que 20 donc ca doit lancer une exception
        Assert.Throws<ArgumentOutOfRangeException>(() => e.AjouterNote(25));
    }

    // NomComplet doit donner le prenom puis le nom en majuscules
    [Fact]
    public void FormatNomComplet()
    {
        Etudiant e = new Etudiant("Lovelace", "Ada", 4);

        Assert.Equal("Ada LOVELACE", e.NomComplet);
    }

    // Deux etudiants avec les memes nom, prenom et naissance sont egaux, donc le HashSet n'en garde qu'un
    [Fact]
    public void HashSetGardeUnSeulEtudiantEgal()
    {
        Etudiant e1 = new Etudiant("Lovelace", "Ada", 4) { Naissance = new DateOnly(1815, 12, 10) };
        Etudiant e2 = new Etudiant("Lovelace", "Ada", 5) { Naissance = new DateOnly(1815, 12, 10) };

        HashSet<Etudiant> ensemble = new HashSet<Etudiant>();
        ensemble.Add(e1);
        bool ajoute = ensemble.Add(e2);

        // e2 est egal a e1 (l'annee ne compte pas) donc il n'est pas ajoute
        Assert.False(ajoute);
        Assert.Single(ensemble);
    }

    // Si la date de naissance est differente, ce ne sont pas les memes etudiants
    [Fact]
    public void EtudiantsDifferents()
    {
        Etudiant e1 = new Etudiant("Lovelace", "Ada", 4) { Naissance = new DateOnly(1815, 12, 10) };
        Etudiant e2 = new Etudiant("Lovelace", "Ada", 4) { Naissance = new DateOnly(1900, 1, 1) };

        HashSet<Etudiant> ensemble = new HashSet<Etudiant> { e1, e2 };

        Assert.Equal(2, ensemble.Count);
    }
}
