using Geometrie;

namespace TpCsharp.Tests;

// Tests de la struct Point2D
public class Point2DTests
{
    // Quand on copie un point puis qu'on change la copie, l'original ne doit pas bouger
    [Fact]
    public void CopieNeModifiePasOriginal()
    {
        Point2D original = new Point2D(1, 2);

        // copie est une copie de original, avec un X different
        Point2D copie = original with { X = 9 };

        Assert.Equal(1, original.X);
        Assert.Equal(9, copie.X);
    }

    // Une forme sans centre a le centre en (0, 0)
    [Fact]
    public void CentreParDefaut()
    {
        Cercle c = new Cercle(1);

        Assert.Equal(0, c.Centre.X);
        Assert.Equal(0, c.Centre.Y);
    }
}
