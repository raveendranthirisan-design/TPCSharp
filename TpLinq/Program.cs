using System.Globalization;

// on lit notes.csv sans boucle : OfType<Note>() enleve les null et donne directement des Note
List<Note> notes = File.ReadLines("notes.csv")
    .Skip(1)
    .Select((ligne, i) =>
    {
        var note = Parser(ligne);
        // i commence a 0 et on a saute l'en-tete, donc la ligne du fichier est i + 2
        if (note is null) Console.Error.WriteLine($"Ligne {i + 2} invalide : {ligne}");
        return note;
    })
    .OfType<Note>()
    .ToList();

// on lit etudiants.csv de la meme facon, toutes les lignes sont valides
List<Inscrit> etudiants = File.ReadLines("etudiants.csv")
    .Skip(1)
    .Select(l => l.Split(','))
    .Select(c => new Inscrit(c[0], c[1], c[2]))
    .ToList();

Console.WriteLine($"{notes.Count} notes valides");
Console.WriteLine($"{etudiants.Count} inscrits");

// sert a afficher une note sous la forme « Ada · Algo · 15 »
Action<Note> afficher = Modele.AffichageNote();


// ---------- Delegues et lambdas ----------

// on teste Filtrer avec une methode nommee, puis avec une lambda
List<Note> enAlgo = Modele.Filtrer(notes, Modele.EstEnAlgo);
List<Note> faibles = Modele.Filtrer(notes, n => n.Valeur < 10);

// on range la troisieme lambda dans une variable : son type est Func<Note, bool>
Func<Note, bool> adaAuDessusDe15 = n => n.Prenom == "Ada" && n.Valeur >= 15;
List<Note> ada = Modele.Filtrer(notes, adaAuDessusDe15);
// var c = n => n.Valeur >= 15; ne compile pas (CS8917) : le compilateur ne connait pas le type de n

Console.WriteLine($"{enAlgo.Count} notes en Algo, {faibles.Count} notes sous 10, {ada.Count} notes d'Ada >= 15");

// on calcule les points ponderes d'une note avec un Func
Func<Note, double> points = Modele.Ponderation();
Console.WriteLine($"Points pondérés de la première note : {points(notes[0])}");

// AuDessusDe renvoie une lambda, qu'on donne directement a Filtrer
List<Note> audessus14 = Modele.Filtrer(notes, Modele.AuDessusDe(14));
Console.WriteLine($"{audessus14.Count} notes >= 14");

// tri sur une copie : par matiere, puis par valeur decroissante
List<Note> copie = new List<Note>(notes);
copie.Sort((a, b) =>
{
    int r = string.Compare(a.Matiere, b.Matiere);
    if (r != 0)
    {
        return r;
    }
    else
    {
        return b.Valeur.CompareTo(a.Valeur);
    }
});
// puis on enleve les notes de Reseau de la copie
copie.RemoveAll(n => n.Matiere == "Reseau");

Console.WriteLine("--- Copie triée, sans Réseau ---");
foreach (Note note in copie) afficher(note);

// la lambda capture la variable seuil (pas sa valeur) : elle voit le changement de 12 a 15
double seuil = 12;
Func<Note, bool> admis = n => n.Valeur >= seuil;
// AuDessusDe recoit une copie de seuil, donc cette lambda garde 12 meme si seuil change
Func<Note, bool> admisA12 = Modele.AuDessusDe(seuil);
Console.WriteLine(Modele.Filtrer(notes, admis).Count);
seuil = 15;
Console.WriteLine(Modele.Filtrer(notes, admis).Count);
Console.WriteLine(Modele.Filtrer(notes, admisA12).Count);


// ---------- Filtrer, projeter, trier ----------

// on affiche les notes d'Algo, de la plus grande a la plus petite
var algo = notes
    .Where(n => n.Matiere == "Algo")
    .OrderByDescending(n => n.Valeur)
    .ToList();

Console.WriteLine("--- Notes d'Algo (décroissant) ---");
foreach (Note note in algo) afficher(note);

// on affiche la liste des matieres, sans doublon et triee
var matieres = notes
    .Select(n => n.Matiere)
    .Distinct()
    .OrderBy(m => m)
    .ToList();

Console.WriteLine("--- Matières ---");
foreach (string m in matieres) Console.WriteLine(m);

// on garde le prenom et la valeur des notes sous 10, dans un type anonyme
var faible = notes
    .Where(n => n.Valeur < 10)
    .Select(n => new { n.Prenom, n.Valeur })
    .ToList();

Console.WriteLine("--- Notes < 10 (type anonyme) ---");
foreach (var x in faible) Console.WriteLine($"{x.Prenom} : {x.Valeur}");

// meme chose avec un tuple
var faibleTuple = notes
    .Where(n => n.Valeur < 10)
    .Select(n => (n.Prenom, n.Valeur))
    .ToList();

Console.WriteLine("--- Notes < 10 (tuple) ---");
foreach (var d in faibleTuple) Console.WriteLine($"{d.Prenom} : {d.Valeur}");

// avec deux OrderBy, le deuxieme refait tout le tri et les matieres sont melangees
var classementFaux = notes
    .OrderBy(n => n.Matiere)
    .OrderByDescending(n => n.Valeur);

Console.WriteLine("--- Classement avec deux OrderBy ---");
foreach (Note note in classementFaux) afficher(note);

// ThenBy garde le tri d'avant et trie seulement les notes egales
var classement = notes
    .OrderBy(n => n.Matiere)
    .ThenByDescending(n => n.Valeur)
    .ThenBy(n => n.Prenom);

Console.WriteLine("--- Classement avec ThenBy ---");
foreach (Note note in classement) afficher(note);

// on trie toutes les notes par valeur decroissante, puis on garde les 3 premieres
var parValeur = notes.OrderByDescending(n => n.Valeur).ToList();
var top3 = parValeur.Take(3).ToList();

Console.WriteLine("--- 3 meilleures notes ---");
foreach (Note note in top3) afficher(note);

// on saute les 3 premieres et on garde les 3 suivantes
var places4a6 = parValeur.Skip(3).Take(3).ToList();

Console.WriteLine("--- Notes de la 4e à la 6e place ---");
foreach (Note note in places4a6) afficher(note);


// ---------- Agreger ----------

// nombre de notes, nombre de notes sous 10 et moyenne simple
int nbSous10 = notes.Count(n => n.Valeur < 10);
double moyenne = notes.Average(n => n.Valeur);
Console.WriteLine($"{notes.Count} notes, dont {nbSous10} sous 10, moyenne {moyenne:F2}");

// Max donne seulement la valeur, MaxBy donne la note complete
double meilleure = notes.Max(n => n.Valeur);
Note? meilleureNote = notes.MaxBy(n => n.Valeur);
Console.WriteLine($"Meilleure note (Max) : {meilleure}");
Console.WriteLine($"Meilleure note (MaxBy) : {meilleureNote}");

// moyenne ponderee : somme des valeurs x coefs, divisee par la somme des coefs
double moyennePonderee = notes.Sum(n => n.Valeur * n.Coef) / notes.Sum(n => n.Coef);
Console.WriteLine($"Moyenne pondérée : {moyennePonderee:F2}");

// prenoms distincts dans l'ordre du fichier, avec Aggregate puis avec string.Join
var prenoms = notes.Select(n => n.Prenom).Distinct();
string avecAggregate = prenoms.Aggregate((acc, p) => acc + ", " + p);
string avecJoin = string.Join(", ", prenoms);
Console.WriteLine($"Aggregate : {avecAggregate}");
Console.WriteLine($"string.Join : {avecJoin}");


// ---------- Chercher et tester ----------

// Linus n'a pas de note en BD : FirstOrDefault renvoie null
Note? linusBD = notes.FirstOrDefault(n => n.Prenom == "Linus" && n.Matiere == "BD");
Console.WriteLine($"Linus en BD (FirstOrDefault) : {linusBD?.ToString() ?? "null"}");

// First leve une exception quand aucune note ne correspond
try
{
    notes.First(n => n.Prenom == "Linus" && n.Matiere == "BD");
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"Linus en BD (First) : {e.Message}");
}

// Single : Ada a exactement une note en Algo
Note adaAlgo = notes.Single(n => n.Prenom == "Ada" && n.Matiere == "Algo");
Console.WriteLine($"Ada en Algo (Single) : {adaAlgo}");

// Single sur toutes les notes d'Ada : il y en a plusieurs, donc exception
try
{
    notes.Single(n => n.Prenom == "Ada");
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"Toutes les notes d'Ada (Single) : {e.Message}");
}

// Any : au moins une note sous 10 ? All : toutes les notes de BD sont >= 12 ?
bool uneSous10 = notes.Any(n => n.Valeur < 10);
bool tous12EnBD = notes.Where(n => n.Matiere == "BD").All(n => n.Valeur >= 12);
// on teste aussi All sur une liste vide
bool allSurVide = new List<Note>().All(n => n.Valeur >= 12);
Console.WriteLine($"Une note sous 10 : {uneSous10}");
Console.WriteLine($"Tous au moins 12 en BD : {tous12EnBD}");
Console.WriteLine($"All sur une liste vide : {allSurVide}");


// ---------- Regrouper ----------

// on regroupe les notes par matiere : chaque groupe est un IGrouping<string, Note>
Console.WriteLine("--- Notes par matière ---");
foreach (IGrouping<string, Note> groupe in notes.GroupBy(n => n.Matiere))
{
    Console.WriteLine(groupe.Key);
    foreach (Note note in groupe) afficher(note);
}

// une ligne de statistiques par matiere, en une seule requete, triee par moyenne decroissante
var stats = notes
    .GroupBy(n => n.Matiere)
    .Select(g => new
    {
        Matiere = g.Key,
        Nombre = g.Count(),
        Moyenne = g.Average(n => n.Valeur),
        Min = g.Min(n => n.Valeur),
        Max = g.Max(n => n.Valeur),
        // MaxBy donne la note la plus haute, on garde seulement son prenom
        Major = g.MaxBy(n => n.Valeur)!.Prenom
    })
    .OrderByDescending(s => s.Moyenne)
    .ToList();

Console.WriteLine("--- Statistiques par matière ---");
foreach (var s in stats)
{
    Console.WriteLine($"{s.Matiere} : {s.Nombre} notes, moyenne {s.Moyenne:F2}, min {s.Min}, max {s.Max}, major {s.Major}");
}

// moyenne ponderee de chaque etudiant, rangee dans un dictionnaire prenom -> moyenne
Dictionary<string, double> moyennes = notes
    .GroupBy(n => n.Prenom)
    .ToDictionary(g => g.Key, g => g.Sum(n => n.Valeur * n.Coef) / g.Sum(n => n.Coef));

Console.WriteLine("--- Moyennes pondérées ---");
foreach (var (prenom, moy) in moyennes.OrderByDescending(p => p.Value))
{
    Console.WriteLine($"{prenom} : {moy:F2}");
}

// nombre de notes par etudiant avec CountBy (.NET 9)
Console.WriteLine("--- Notes par étudiant (CountBy) ---");
foreach (var (prenom, nb) in notes.CountBy(n => n.Prenom))
{
    Console.WriteLine($"{prenom} : {nb}");
}

// meme chose avec GroupBy puis Count
Console.WriteLine("--- Notes par étudiant (GroupBy + Count) ---");
foreach (var groupe in notes.GroupBy(n => n.Prenom))
{
    Console.WriteLine($"{groupe.Key} : {groupe.Count()}");
}


// ---------- Joindre ----------

// on joint les notes et les inscrits sur la cle (nom, prenom) pour faire un releve
var releve = notes
    .Join(etudiants,
        n => new { n.Nom, n.Prenom },
        i => new { i.Nom, i.Prenom },
        (n, i) => new { n.Prenom, i.Promo, n.Matiere, n.Valeur })
    .ToList();

Console.WriteLine("--- Relevé ---");
foreach (var ligne in releve)
{
    Console.WriteLine($"{ligne.Prenom} ({ligne.Promo}) · {ligne.Matiere} · {ligne.Valeur}");
}

// moyenne simple de chaque promo, a partir du releve
Console.WriteLine("--- Moyenne par promo ---");
foreach (var promo in releve.GroupBy(r => r.Promo).OrderBy(g => g.Key))
{
    Console.WriteLine($"{promo.Key} : {promo.Average(r => r.Valeur):F2}");
}

// inscrits sans aucune note, avec Where et Any
var sansNote = etudiants.Where(i => !notes.Any(n => n.Nom == i.Nom && n.Prenom == i.Prenom));
foreach (Inscrit inscrit in sansNote)
{
    Console.WriteLine($"Sans note (Where + Any) : {inscrit.Prenom} {inscrit.Nom}");
}

// meme chose avec LeftJoin (.NET 10) : un inscrit sans note est garde, avec une note null
var sansNoteLeftJoin = etudiants
    .LeftJoin(notes,
        i => new { i.Nom, i.Prenom },
        n => new { n.Nom, n.Prenom },
        (i, n) => new { Inscrit = i, Note = n })
    .Where(x => x.Note is null)
    .Select(x => x.Inscrit);

foreach (Inscrit inscrit in sansNoteLeftJoin)
{
    Console.WriteLine($"Sans note (LeftJoin) : {inscrit.Prenom} {inscrit.Nom}");
}

// index prenom -> notes avec ToLookup
ILookup<string, Note> index = notes.ToLookup(n => n.Prenom);

Console.WriteLine("--- Notes de Grace (ToLookup) ---");
foreach (Note note in index["Grace"]) afficher(note);

// pour un prenom absent, l'index renvoie une sequence vide et pas une exception
Console.WriteLine($"Notes de Margaret dans l'index : {index["Margaret"].Count()}");


// ---------- SelectMany ----------

// un bulletin par etudiant : on regroupe par prenom puis on garde la liste des valeurs
List<Bulletin> bulletins = notes
    .GroupBy(n => n.Prenom)
    .Select(g => new Bulletin(g.Key, g.Select(n => n.Valeur).ToList()))
    .ToList();

// SelectMany met toutes les listes bout a bout : on obtient un IEnumerable<double>
IEnumerable<double> toutesLesValeurs = bulletins.SelectMany(b => b.Valeurs);
// Select garde une liste par bulletin : on obtient un IEnumerable<List<double>>
IEnumerable<List<double>> listesDeValeurs = bulletins.Select(b => b.Valeurs);

Console.WriteLine($"SelectMany : {string.Join(", ", toutesLesValeurs)}");
Console.WriteLine($"Select : {listesDeValeurs.Count()} listes");

// seconde forme de SelectMany : on garde le prenom du bulletin avec chaque valeur
var paires = bulletins.SelectMany(b => b.Valeurs, (b, v) => (b.Prenom, v));

Console.WriteLine("--- Paires (prénom, valeur) ---");
foreach (var (prenom, valeur) in paires)
{
    Console.WriteLine($"{prenom} : {valeur}");
}


// ---------- Evaluation differee ----------

// bonnes est une recette : elle est recalculee a chaque Count(), sur l'etat actuel de notes
var bonnes = notes.Where(n => n.Valeur >= 15);
Console.WriteLine(bonnes.Count());
notes.Add(new Note("Hamilton", "Margaret", "Algo", 19, 3));
Console.WriteLine(bonnes.Count());
// ToList execute la requete tout de suite : figees ne change plus ensuite
var figees = notes.Where(n => n.Valeur >= 15).ToList();
notes.Add(new Note("Hamilton", "Margaret", "BD", 20, 2));
Console.WriteLine(bonnes.Count() + " / " + figees.Count);

// effet de bord dans le Where pour voir quand le filtre s'execute
var testees = notes.Where(n =>
{
    Console.Write($"test de {n.Prenom} · ");
    return n.Valeur >= 15;
});

// Count() deux fois : le filtre repasse sur toutes les notes a chaque fois
Console.WriteLine("--- Count() deux fois ---");
Console.WriteLine($"=> {testees.Count()}");
Console.WriteLine($"=> {testees.Count()}");

// ToList() d'abord : le filtre ne passe qu'une fois, les Count suivants lisent la liste
Console.WriteLine("--- ToList() puis Count deux fois ---");
List<Note> testeesListe = testees.ToList();
Console.WriteLine($"=> {testeesListe.Count}");
Console.WriteLine($"=> {testeesListe.Count}");

// Count() > 0 teste toutes les notes, Any() s'arrete a la premiere qui passe
Console.WriteLine("--- Count() > 0 ---");
Console.WriteLine($"=> {testees.Count() > 0}");
Console.WriteLine("--- Any() ---");
Console.WriteLine($"=> {testees.Any()}");

// on compte les notes qui passent par le tri, avec un compteur dans la cle du OrderBy
int trieesWhereAvant = 0;
var whereAvant = notes
    .Where(n => n.Valeur >= 15)
    .OrderBy(n => { trieesWhereAvant++; return n.Valeur; })
    .ToList();

int trieesWhereApres = 0;
var whereApres = notes
    .OrderBy(n => { trieesWhereApres++; return n.Valeur; })
    .Where(n => n.Valeur >= 15)
    .ToList();

Console.WriteLine($"Where puis OrderBy : {trieesWhereAvant} notes triées");
Console.WriteLine($"OrderBy puis Where : {trieesWhereApres} notes triées");
Console.WriteLine($"Même résultat : {whereAvant.SequenceEqual(whereApres)}");


// Transforme une ligne en Note, ou null si la ligne est invalide.
static Note? Parser(string ligne)
{
    var c = ligne.Split(',');
    if (c.Length != 5) return null;
    if (!double.TryParse(c[3], CultureInfo.InvariantCulture, out var v)) return null;
    if (v < 0 || v > 20) return null;
    if (!int.TryParse(c[4], out var coef)) return null;
    return new Note(c[0], c[1], c[2], v, coef);
}
