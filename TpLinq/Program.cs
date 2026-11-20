using System.Globalization;

// Q1 + Q2 : OfType<Note>() écarte les null et donne un List<Note> (et non List<Note?>)
List<Note> notes = File.ReadLines("notes.csv")
    .Skip(1)
    .Select((ligne, i) =>
    {
        var note = Parser(ligne);
        if (note is null) Console.Error.WriteLine($"Ligne {i + 2} invalide : {ligne}");
        return note;
    })
    .OfType<Note>()
    .ToList();
    

// Q3 : toutes les lignes sont valides, pas besoin de Parser ni de filtre
List<Inscrit> etudiants = File.ReadLines("etudiants.csv")
    .Skip(1)
    .Select(l => l.Split(','))
    .Select(c => new Inscrit(c[0], c[1], c[2]))
    .ToList();

Console.WriteLine($"{notes.Count} notes valides");
Console.WriteLine($"{etudiants.Count} inscrits");

static Note? Parser(string ligne)
{
    var c = ligne.Split(',');
    if (c.Length != 5) return null;
    if (!double.TryParse(c[3], CultureInfo.InvariantCulture, out var v)) return null;
    if (v < 0 || v > 20) return null;
    if (!int.TryParse(c[4], out var coef)) return null;
    return new Note(c[0], c[1], c[2], v, coef);
}


 List<Note> copieList =  new List<Note>(notes);
 copieList.Sort( (a, b) => {
    int r = string.Compare(a.Matiere, b.Matiere);
    if(r  != 0)
     {
         return r;
     }
     else
     {
        r = b.Valeur.CompareTo(a.Valeur);
        return r;
     }
 } );

 






// 2.1 notes d'Algo, triées par valeur décroissante

var algo = notes
    .Where(n => n.Matiere == "Algo") 
    .OrderByDescending(n => n.Valeur)
    .ToList();

Console.WriteLine("--- Notes d'Algo (décroissant) ---");
var afficher = Modele.AffichageNote();
foreach (Note n in algo) afficher(n);


var matiere = notes
    .Select(n => n.Matiere)
    .Distinct()
    .OrderBy(m => m)
    .ToList();

Console.WriteLine("--- Matières ---");
foreach (string m in matiere) Console.WriteLine(m);

// List<Note> faibles =Modele.Filtrer(notes,n => n.Valeur  < 10);
// List<Note> ada = Modele.Filtrer(notes,n => n.Prenom == "Ada" && n.Valeur >= 15);

// // Q : la lambda stockée dans une variable. Type : Func<Note, bool>
// var plusDe15 = (Note n) => n.Valeur >= 15;
// List<Note> excellentes = Modele.Filtrer(notes, plusDe15);

// List<Note> audessus14 = Modele.Filtrer(notes, Modele.AuDessusDe(14));
// Console.WriteLine($"{audessus14.Count} notes >= 14");

// Console.WriteLine($"{ada.Count} notes d'Ada supérieures à 15");
// Console.WriteLine($"{excellentes.Count} notes >= 15 ({plusDe15.GetType().Name})");


