using Ecole;    
public class Program
{
    public static string Mention(double moyenne)
    {
        return moyenne switch
        {
            < 0 or > 20 => "Moyenne invalide",
            < 10 => "Ajourné",
            < 12 => "Passable",
            < 14 => "Assez bien",
            < 16 => "Bien",
            <= 20 => "Très bien",
            _ => "Moyenne invalide" // NaN
        };
    }

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
        // ada.Naissance = ...; // idem    }
}

}
