using Microsoft.EntityFrameworkCore;



using (var db = new PromoContext())
{
 if (!db.Etudiants.Any())
 {
 var promo = Donnees.Promo();
 db.Etudiants.AddRange(promo);
 Console.WriteLine($"Avant : {db.Entry(promo[0]).State}");
 int n = db.SaveChanges();
 Console.WriteLine($"{n} lignes écrites");
 Console.WriteLine($"Après : {db.Entry(promo[0]).State}, Id = {promo[0].Id}");
 }
}



using(var db = new PromoContext())
{
    var trier = db.Etudiants
                .Where(e => e.Promo == "M2")
                .OrderBy(e => e.Nom);


  var nbNotes = db.Etudiants
                .Select(e => new {Nom = e.Nom ,NbNote = e.Notes.Count() });



   Console.WriteLine(trier.ToQueryString());

    foreach (var e in trier)
    {
        Console.WriteLine($"{e.Nom} {e.Prenom} ({e.Promo})");
    }

  foreach (var e in nbNotes)
    {
        Console.WriteLine($"{e.Nom} {e.NbNote}");
    }


var moyenne = db.Notes
              .GroupBy(n => n.Matiere)
              .Select(g => new {Nom = g.Key, Moy = g.Average(n => n.Valeur)});



foreach( var e in moyenne)
    {
        Console.WriteLine($"{e.Nom} {e.Moy}");
    }

}