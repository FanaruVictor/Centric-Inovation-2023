using InPositionChatBot.Data.Context;
using InPositionChatBot.Domain.Entities;
using InPositionChatBot.Domain.IRepositories;

namespace InPositionChatBot.Data.Repositories
{
	public class MessageRepository : BaseRepository<Message>, IMessageRepository
	{
		public MessageRepository(InPositionChatBotDbContext context) : base(context)
		{
		}
	}
}
