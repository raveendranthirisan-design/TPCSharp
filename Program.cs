using Ecole;

// Point d'entree du programme. Contient aussi la methode Mention qu'on reutilise dans les autres classes.
public class Program
{
    // Donne la mention qui correspond a une moyenne.
    public static string Mention(double moyenne)
    {
        // le "not" attrape aussi NaN, comme ca il ne passe pas dans le switch
        if (!(moyenne >= 0 && moyenne <= 20))
        {
            throw new ArgumentOutOfRangeException(nameof(moyenne));
        }

        return moyenne switch
        {
            >= 16 => "Très bien",
            >= 14 => "Bien",
            >= 12 => "Assez bien",
            >= 10 => "Passable",
            _ => "Ajourné"
        };
    }

    // Teste la classe Etudiant puis lance les autres exemples.
    public static void Main()
    {
        var ada = new Etudiant("Lovelace", "Ada", 4)
        {
            Naissance = new DateOnly(1815, 12, 10)
        };
        ada.AjouterNote(15);
        ada.AjouterNote(16);
        Console.WriteLine(ada); // appelle ToString()
        ada.Annee = 5; // autorisé
        // ada.Nom = "Byron"; // doit provoquer une erreur de compilation
        // ada.Naissance = ...; // idem

        Exercice3.Run();
        Exercice5.Run();
        Exercice6.Run();
    }
}
