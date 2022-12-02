using HabitTrainer.Domain;
using Microsoft.EntityFrameworkCore;

namespace HabitTrainer.API.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Habit> Habits { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
