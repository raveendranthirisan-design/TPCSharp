using System.Text.Json;

// Sauvegarde et relit les notes dans un fichier JSON.
public static class Stockage
{
    // options partagees : noms en camelCase (conventions du Web) et JSON indente pour etre lisible
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web) { WriteIndented = true };

    // Transforme les notes en texte JSON et l'ecrit dans le fichier.
    public static void Sauver(string chemin, List<Note> notes)
    {
        string json = JsonSerializer.Serialize(notes, Options);
        File.WriteAllText(chemin, json);
    }

    // Lit le fichier JSON et le transforme en liste de notes.
    public static List<Note> Charger(string chemin)
    {
        string json = File.ReadAllText(chemin);
        // si le fichier contient "null", on renvoie une liste vide
        return JsonSerializer.Deserialize<List<Note>>(json, Options) ?? new List<Note>();
    }

    // Meme chose que Sauver, mais sans bloquer le programme pendant l'ecriture.
    public static async Task SauverAsync(string chemin, List<Note> notes)
    {
        string json = JsonSerializer.Serialize(notes, Options);
        await File.WriteAllTextAsync(chemin, json);
    }

    // Meme chose que Charger, mais lit le JSON directement depuis le fichier ouvert.
    public static async Task<List<Note>> ChargerAsync(string chemin)
    {
        // await using referme le fichier tout seul a la fin
        await using FileStream flux = File.OpenRead(chemin);
        return await JsonSerializer.DeserializeAsync<List<Note>>(flux, Options) ?? new List<Note>();
    }
}
