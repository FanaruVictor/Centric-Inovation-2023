using InPositionChatBot.Data.Context;
using InPositionChatBot.Data.Repositories;
using InPositionChatBot.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InPositionChatBot.Data
{
	public static class ServicesConfiguration
	{
		public static void AddDb(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["ConnectionString"];
			var migrationsAssembly = typeof(InPositionChatBotDbContext).GetTypeInfo().Assembly.GetName().Name;
			services.AddDbContext<InPositionChatBotDbContext>(options => options.UseSqlServer(connectionString, sql =>
			{
				sql.MigrationsAssembly(migrationsAssembly);
				sql.MigrationsHistoryTable("__EFMigrationHistory");
			}));

			services.AddScoped<IMessageRepository, MessageRepository>();
		}
	}
}
