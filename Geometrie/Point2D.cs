namespace Geometrie;

// Un point du plan avec X et Y. C'est une struct donc quand on la copie, la copie est independante de l'original.
// Elle est readonly donc on ne peut pas changer X et Y apres la creation.
public readonly struct Point2D
{
    // La position sur l'axe horizontal
    public double X { get; init; }
    // La position sur l'axe vertical
    public double Y { get; init; }

    // Cree un point avec ses deux coordonnees.
    public Point2D(double x, double y)
    {
        X = x;
        Y = y;
    }
}
