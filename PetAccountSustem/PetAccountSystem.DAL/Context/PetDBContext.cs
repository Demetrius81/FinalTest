using Microsoft.EntityFrameworkCore;
using PetAccountSystem.Models.Models;

namespace PetAccountSystem.DAL.Context;
public class PetDBContext : DbContext
{
	public DbSet<Pet> Pets { get; set; }

	public PetDBContext(DbContextOptions<PetDBContext> opt) : base(opt)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Pet>().Property(p => p.KindOfAnimal).IsRequired();
		modelBuilder.Entity<Pet>().Property(p => p.IsPackAnimal).IsRequired();
		modelBuilder.Entity<Pet>().Property(p => p.Count).IsRequired();
	}
}
