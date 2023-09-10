namespace InPositionChatBot.Domain.Entities
{
	public enum Sender
	{
		AI,
		User
	}

	public class Message : BaseEntity
	{
		public Guid Id { get; }
		public Sender Sender { get; }
		public string Text { get; }
		public DateTime Date { get; }

		public Message()
		{

		}

		public Message(Guid id, Sender sender, string text, DateTime date)
		{
			Id = id;
			Sender = sender;
			Text = text;
			Date = date;
		}
	}
}
