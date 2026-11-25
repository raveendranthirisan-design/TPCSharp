using Microsoft.EntityFrameworkCore;


// ---------- Ajouter des donnees ----------

// on remplit la base au premier lancement seulement : Any() renvoie vrai des que la table a une ligne
using (var db = new PromoContext())
{
    if (!db.Etudiants.Any())
    {
        var promo = Donnees.Promo();
        // AddRange n'envoie rien a la base : le contexte suit les etudiants et leurs notes (etat Added)
        db.Etudiants.AddRange(promo);
        Console.WriteLine($"Avant : {db.Entry(promo[0]).State}");
        // SaveChanges envoie tous les INSERT dans une transaction et renvoie le nombre de lignes ecrites
        int n = db.SaveChanges();
        Console.WriteLine($"{n} lignes écrites");
        // apres l'enregistrement l'etudiant est Unchanged, et SQLite lui a donne son Id
        Console.WriteLine($"Après : {db.Entry(promo[0]).State}, Id = {promo[0].Id}");
    }
}


// ---------- Interroger ----------

// un contexte neuf ne connait aucun objet : tout ce qu'on veut, il doit le relire dans la base
using (var db = new PromoContext())
{
    // les etudiants de M2 tries par nom : Where et OrderBy deviennent WHERE et ORDER BY en SQL
    var trier = db.Etudiants
        .Where(e => e.Promo == "M2")
        .OrderBy(e => e.Nom);

    // ToQueryString donne le SQL sans executer la requete, c'est le foreach qui l'envoie a la base
    Console.WriteLine(trier.ToQueryString());

    foreach (var e in trier)
    {
        Console.WriteLine($"{e.Nom} {e.Prenom} ({e.Promo})");
    }

    // le nom et le nombre de notes de chaque etudiant, dans un type anonyme
    // pas besoin d'Include : Count() devient une sous-requete SELECT COUNT(*) dans le SQL
    var nbNotes = db.Etudiants
        .Select(e => new { Nom = e.Nom, NbNote = e.Notes.Count() });

    foreach (var e in nbNotes)
    {
        Console.WriteLine($"{e.Nom} {e.NbNote}");
    }

    // la moyenne de chaque matiere : le calcul est fait par SQLite (GROUP BY et AVG), pas en C#
    var moyenne = db.Notes
        .GroupBy(n => n.Matiere)
        .Select(g => new { Nom = g.Key, Moy = g.Average(n => n.Valeur) });

    foreach (var e in moyenne)
    {
        Console.WriteLine($"{e.Nom} {e.Moy}");
    }

    // sans Include, Notes reste vide (0 partout) car EF ne charge jamais les relations tout seul
    // avec Include, EF fait une jointure entre Etudiants et Notes et remplit la liste Notes
    var parcourir = db.Etudiants.Include(e => e.Notes).ToList();

    foreach (var e in parcourir)
    {
        Console.WriteLine($"{e.Notes.Count}");
    }

    // moyenne ponderee de chaque etudiant qui a au moins une note, de la meilleure a la moins bonne
    // le Where enleve Hamilton : sans notes, on diviserait par une somme de coefs egale a 0
    var MoyPonderee = db.Etudiants
        .Where(e => e.Notes.Count() > 0)
        .Select(e => new { Nom = e.Nom, Moy = e.Notes.Sum(s => s.Valeur * s.Coef) / e.Notes.Sum(n => n.Coef) })
        .OrderByDescending(x => x.Moy);

    foreach (var e in MoyPonderee)
    {
        // :F2 affiche toujours deux decimales (arrondi)
        Console.WriteLine($"{e.Nom} {e.Moy:F2}");
    }
}


// ---------- Modifier et supprimer ----------

using (var db = new PromoContext())
{
    // Alan Turing passe en M2 : on le charge, on change sa promo, le contexte compare avec les valeurs d'origine
    var alan = db.Etudiants.First(e => e.Prenom == "Alan");
    alan.Promo = "M2";
    // Modified au premier lancement, puis Unchanged car Alan est deja en M2 dans la base
    Console.WriteLine($"Avant SaveChanges : {db.Entry(alan).State}");

    // deja execute une fois : Linus n'existe plus, donc First planterait (Sequence contains no elements)
    // ses 2 notes ont ete supprimees avec lui (cascade) : il reste 11 notes dans la base
    // var supprimer =  db.Etudiants.First(e => e.Prenom == "Linus" );
    // db.Remove(supprimer);
    // int n = db.SaveChanges();

    // coef 2 pour toutes les notes de Reseau en un seul UPDATE, sans charger les notes en memoire
    // ExecuteUpdate part tout de suite dans la base : pas besoin de SaveChanges
    var coef = db.Notes.Where(e => e.Matiere == "Reseau").ExecuteUpdate(s => s.SetProperty(n => n.Coef, 2));
}
