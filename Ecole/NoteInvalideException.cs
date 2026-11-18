namespace Ecole;

// Exception lancee quand une ligne du fichier de notes est fausse.
public class NoteInvalideException : Exception
{
    // Le numero de la ligne fautive dans le fichier
    public int Ligne { get; }

    public NoteInvalideException(int ligne)
    {
        Ligne = ligne;
    }
}
