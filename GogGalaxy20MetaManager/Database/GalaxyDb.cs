using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace GogGalaxy20MetaManager
{
	public class GalaxyDb: DbContext
	{
		public DbSet<WebCacheResources> WebCacheResources { get; set; }
		public DbSet<WebCache> WebCache { get; set; }
		public DbSet<GamePieces> GamePieces { get; set; }
		public DbSet<Users> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			var dbPath = Path.Combine(rootFolder, "GOG.com", "Galaxy", "storage", "galaxy-2.0.db");
			optionsBuilder.UseSqlite($"Data Source=\"{dbPath}\"");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Users>().HasKey(u => u.Id);
			modelBuilder.Entity<GamePieces>().HasKey(p => new {p.ReleaseKey, p.GamePieceTypeId, p.UserId});
			modelBuilder.Entity<WebCache>().HasKey(r => r.Id);
			modelBuilder.Entity<WebCache>().HasIndex(r => new {r.ReleaseKey, r.UserId}).IsUnique();
			modelBuilder.Entity<WebCacheResources>().HasKey(r => new {r.WebCacheId, r.WebCacheResourceTypeId});
			modelBuilder.Entity<WebCacheResources>().HasOne(r => r.WebCache);
			//modelBuilder.Entity<WebCacheResources>().HasNoKey();
			//modelBuilder.Entity<WebCacheResources>().HasIndex(r => new {r.WebCacheId, r.WebCacheResourceTypeId}).IsUnique();

			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			foreach (var property in entity.GetProperties())
				property.SetColumnName(NamingStyles.CamelCase(property.Name));
		}
	}
}
