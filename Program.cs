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
        Exercice3.Run();
    }
}
