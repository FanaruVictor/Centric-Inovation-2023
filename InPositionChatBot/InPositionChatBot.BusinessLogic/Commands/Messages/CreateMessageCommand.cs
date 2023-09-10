using InPositionChatBot.Domain.Entities;
using InPositionChatBot.Domain.IRepositories;
using MediatR;

namespace InPositionChatBot.BusinessLogic.Commands.Messages
{
	public class CreateMessageCommand : IRequest<bool>
	{
		public Guid Id { get; set; }
		public Sender Sender { get; set; }
		public string Text { get; set; }
		public DateTime Date { get; set; }
	}

	public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, bool>
	{
		private readonly IMessageRepository _repository;

		public CreateMessageCommandHandler(IMessageRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
		{
			if (request.Date == null)
			{
				request.Date = DateTime.UtcNow;
			}

			var message = new Message(request.Id, request.Sender, request.Text, request.Date);

			await _repository.Add(message);
			await _repository.SaveChanges();

			return true;
		}
	}
}
