using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
public class PromoContext : DbContext
{
 public DbSet<Etudiant> Etudiants => Set<Etudiant>();
 public DbSet<Note> Notes => Set<Note>();
 protected override void OnConfiguring(DbContextOptionsBuilder options)
 => options.UseSqlite("Data Source=promo.db")
 .LogTo(Console.WriteLine, LogLevel.Information);
}