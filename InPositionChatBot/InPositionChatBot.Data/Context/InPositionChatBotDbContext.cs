using InPositionChatBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InPositionChatBot.Data.Context
{
	public class InPositionChatBotDbContext : DbContext
	{
		public InPositionChatBotDbContext(DbContextOptions options) : base(options) { }
		public DbSet<Message> Messages => Set<Message>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
