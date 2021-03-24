using DAL.Models.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class AvertoryDbContext : IdentityDbContext<User>
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Inventory> Inventories { get; set; }

		public AvertoryDbContext(DbContextOptions<AvertoryDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Inventory>()
				.HasIndex(i => i.Identifier)
				.IsUnique();

			builder.Entity<Product>()
				.HasIndex(p => new { p.CompanyPrefix, p.ItemReference})
				.IsUnique();

			builder.Entity<Item>()
				.HasIndex(i => i.SerialNumber)
				.IsUnique();
		}

	}
}
