namespace Geometrie;

// Un carre est un rectangle qui a les deux cotes egaux. Elle est sealed donc personne ne peut en heriter.
public sealed class Carre : Rectangle
{
    // Cree un carre.
    public Carre(double cote) : base(cote, cote)
    {
    }
}
