using System.Globalization;
using System.Text.Json;
using System.Diagnostics;
using System.Net.Http.Json;

// on charge promo.json s'il existe, sinon on importe notes.csv et on cree promo.json
List<Note> notes;

if (File.Exists("promo.json"))
{
    try
    {
        notes = await Stockage.ChargerAsync("promo.json");
        Console.WriteLine("Notes chargées depuis promo.json");
    }
    catch (JsonException e)
    {
        // le JSON est abime (accolade en moins...) : on repart de notes.csv
        Console.Error.WriteLine($"promo.json est illisible ({e.Message}), on repart de notes.csv");
        notes = LectureNote();
        await Stockage.SauverAsync("promo.json", notes);
    }
    catch (ArgumentOutOfRangeException e)
    {
        // une note hors 0-20 dans le JSON est refusee par le record : on repart aussi de notes.csv
        Console.Error.WriteLine($"promo.json contient une note invalide ({e.Message}), on repart de notes.csv");
        notes = LectureNote();
        await Stockage.SauverAsync("promo.json", notes);
    }
}
else
{
    // premier lancement : on importe le CSV puis on cree le JSON
    notes = LectureNote();
    await Stockage.SauverAsync("promo.json", notes);
    Console.WriteLine("notes.csv importé, promo.json créé");
}

Console.WriteLine($"{notes.Count} notes valides");

// on lance les 3 operations en meme temps, puis on les attend ensemble : environ 1 s au lieu de 3 s
var chrono = Stopwatch.StartNew();

Task<string> a = OperationLenteAsync("A");
Task<string> b = OperationLenteAsync("B");
Task<string> c = OperationLenteAsync("C");

string[] resultats = await Task.WhenAll(a, b, c);

foreach (string r in resultats) Console.WriteLine(r);
Console.WriteLine($"Durée : {chrono.ElapsedMilliseconds} ms");

// un seul HttpClient pour tout le programme, avec l'adresse de base de l'API
HttpClient client = new HttpClient { BaseAddress = new Uri("https://geo.api.gouv.fr/") };

// on demande les communes du code postal 35000, GetFromJsonAsync deserialise directement en List<Commune>
List<Commune>? communes = await client.GetFromJsonAsync<List<Commune>>("communes?codePostal=35000");

Console.WriteLine("--- Communes du 35000 ---");
foreach (Commune commune in communes ?? new List<Commune>())
{
    Console.WriteLine($"{commune.Nom} ({commune.Code}) : {commune.Population} habitants");
}

// on lit etudiants.csv, le code postal est la 4e colonne
// List<Inscrit> etudiants = File.ReadLines("etudiants.csv")
//     .Skip(1)
//     .Select(l => l.Split(','))
//     .Select(c => new Inscrit(c[0], c[1], c[2], c[3]))
//     .ToList();
// Console.WriteLine($"{etudiants.Count} inscrits");

// tests du record Note : les deux lignes levent une ArgumentOutOfRangeException
// try { new Note("Lovelace", "Ada", "Algo", 25, 3); }
// catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.Message); }
// try { new Note("Lovelace", "Ada", "Algo", 15, 0); }
// catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.Message); }

// Transforme une ligne en Note, ou leve une FormatException qui dit pourquoi la ligne est invalide.
// Une note hors de 0-20 n'est pas testee ici : c'est le record Note qui leve l'exception.
static Note ParserStrict(string ligne, int numero)
{
    var c = ligne.Split(',');
    if (c.Length != 5)
        throw new FormatException($"Ligne {numero} : 5 champs attendus, {c.Length} trouvés");
    // TryParse car une note mal ecrite dans le CSV est un cas prevu, pas exceptionnel
    if (!double.TryParse(c[3], CultureInfo.InvariantCulture, out var v))
        throw new FormatException($"Ligne {numero} : note non numérique ({c[3]})");
    if (!int.TryParse(c[4], out var coef))
        throw new FormatException($"Ligne {numero} : coef non entier ({c[4]})");
    return new Note(c[0], c[1], c[2], v, coef);
}

// Lit notes.csv ligne par ligne, garde les notes valides et signale les autres sur Console.Error.
static List<Note> LectureNote()
{
    List<Note> notes = new List<Note>();
    int numero = 1; // la ligne 1 est l'en-tete

    foreach (string ligne in File.ReadLines("notes.csv").Skip(1))
    {
        numero++;
        try
        {
            Note note = ParserStrict(ligne, numero);
            notes.Add(note);
        }
        // on n'attrape que les erreurs prevues, pas Exception, pour ne pas cacher les vrais bugs
        catch (FormatException e)
        {
            Console.Error.WriteLine(e.Message);
        }
        catch (ArgumentOutOfRangeException e)
        {
            // le record ne connait pas le numero de ligne, on l'ajoute ici
            Console.Error.WriteLine($"Ligne {numero} : {e.Message}");
        }
    }

    return notes;
}

// Simule une operation lente (comme un appel reseau) qui dure 1 seconde.
static async Task<string> OperationLenteAsync(string nom)
{
    await Task.Delay(1000);
    return $"{nom} terminée";
}
