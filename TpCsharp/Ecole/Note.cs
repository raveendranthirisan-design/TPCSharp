using System.Globalization;

namespace Ecole;

// Une note d'un etudiant dans une matiere. C'est un record donc elle ne change pas
// et deux notes avec les memes donnees sont egales.
public record Note(string Nom, string Prenom, string Matiere, double Valeur, int Coef)
{
    // Transforme une ligne du fichier en Note, ou lance une exception si elle est fausse.
    public static Note LireLigne(string ligne, int numero)
    {
        string[] champs = ligne.Split(',');

        if (champs.Length != 5)
        {
            throw new NoteInvalideException(numero);
        }

        // InvariantCulture pour que le point soit le separateur des decimales
        if (!double.TryParse(champs[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double valeur))
        {
            throw new NoteInvalideException(numero);
        }

        if (valeur < 0 || valeur > 20)
        {
            throw new NoteInvalideException(numero);
        }

        // si le coef n'est pas un entier la ligne est fausse aussi
        if (!int.TryParse(champs[4], out int coef))
        {
            throw new NoteInvalideException(numero);
        }

        return new Note(champs[0], champs[1], champs[2], valeur, coef);
    }
}
