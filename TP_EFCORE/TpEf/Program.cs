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