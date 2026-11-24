// Une note d'un etudiant dans une matiere. La note et le coef sont verifies des la creation.
public record Note(string Nom, string Prenom, string Matiere, double Valeur, int Coef)
{
    // on redeclare Valeur pour verifier qu'elle est entre 0 et 20, sinon exception
    public double Valeur { get; init; } = Valeur >= 0 && Valeur <= 20
        ? Valeur
        : throw new ArgumentOutOfRangeException(nameof(Valeur), "La note doit etre entre 0 et 20");

    // on redeclare Coef pour verifier qu'il est strictement positif
    public int Coef { get; init; } = VerifierCoef(Coef);

    // Leve une exception si le coef est <= 0, sinon le renvoie.
    static int VerifierCoef(int coef)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(coef);
        return coef;
    }
}

// Un etudiant inscrit, avec son code postal.
public record Inscrit(string Nom, string Prenom, string Promo, string CodePostal);

// Une commune renvoyee par l'API geo.api.gouv.fr (on garde seulement les champs utiles).
// Code est un string car un code INSEE peut commencer par 0 ou contenir une lettre (2A004).
public record Commune(string Nom, string Code, int Population);
