using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// La session de travail avec la base : elle donne acces aux tables et suit les objets charges.
public class PromoContext : DbContext
{
    // Les tables de la base, une par entite.
    public DbSet<Etudiant> Etudiants => Set<Etudiant>();
    public DbSet<Note> Notes => Set<Note>();

    // On utilise SQLite avec le fichier promo.db, et on affiche dans la console le SQL envoye.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=promo.db")
                  .LogTo(Console.WriteLine, LogLevel.Information);
}
