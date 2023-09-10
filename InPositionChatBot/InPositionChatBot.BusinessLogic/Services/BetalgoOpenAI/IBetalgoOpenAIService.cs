namespace InPositionChatBot.BusinessLogic.Services.BetalgoOpenAI
{
	public interface IBetalgoOpenAIService
	{
		public Task<string> SendMessage(string message);
	}
}
