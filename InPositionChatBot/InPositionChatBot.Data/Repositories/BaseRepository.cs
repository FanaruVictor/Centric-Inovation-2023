using InPositionChatBot.Data.Context;
using InPositionChatBot.Domain.Entities;
using InPositionChatBot.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace InPositionChatBot.Data.Repositories
{
	public class BaseRepository<TEnitity> : IBaseRepository<TEnitity>
		where TEnitity : BaseEntity
	{
		private readonly InPositionChatBotDbContext _context;

		public BaseRepository(InPositionChatBotDbContext context)
		{
			_context = context;
		}

		public async Task Add(TEnitity entity)
		{
			await _context.AddAsync(entity);
		}

		public async Task Delete(Guid id)
		{
			var enitity = await GetById(id);

			if (enitity == null)
			{
				throw new Exception("No entity found with specified id");
			}

			_context.Set<TEnitity>().Remove(enitity);
		}

		public async Task<List<TEnitity>> GetAll()
		{
			return await _context.Set<TEnitity>().AsNoTracking().ToListAsync();
		}

		public async Task<TEnitity?> GetById(Guid id)
		{
			return await _context.Set<TEnitity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<int> SaveChanges()
		{
			return await _context.SaveChangesAsync();
		}

		public void Update(TEnitity entity)
		{
			_context.Set<TEnitity>().Update(entity);
		}
	}
}
