using Geometrie;

// Teste le polymorphisme avec une liste de formes.
public static class Exercice5
{
    // Trie les formes par aire, les dessine, puis calcule l'aire totale et la forme qui a le plus grand perimetre.
    public static void Run()
    {
        List<Forme> formes = [new Cercle(1), new Rectangle(2, 5), new Carre(3), new Cercle(2.5)];
        formes.Sort(); // utilise CompareTo

        foreach (var f in formes)
        {
            f.Dessiner();
        }

        double aireTotale = 0;
        Forme? plusGrand = null;

        foreach (Forme f in formes)
        {
            aireTotale += f.Aire();
            if (plusGrand == null || f.Perimetre() > plusGrand.Perimetre())
            {
                plusGrand = f;
            }
        }

        Console.WriteLine("Aire totale : " + aireTotale.ToString("F2"));
        Console.WriteLine("Plus grand périmètre : " + plusGrand!.Decrire());
    }
}
