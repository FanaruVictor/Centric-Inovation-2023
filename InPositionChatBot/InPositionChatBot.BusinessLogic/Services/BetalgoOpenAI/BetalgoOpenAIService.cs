using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace InPositionChatBot.BusinessLogic.Services.BetalgoOpenAI
{
	public class BetalgoOpenAIService : IBetalgoOpenAIService
	{
		private readonly IOpenAIService _opentAiService;

		public BetalgoOpenAIService(IOpenAIService opentAiService)
		{
			_opentAiService = opentAiService;
		}
		public async Task<string> SendMessage(string message)
		{
			var completionResult = await _opentAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
			{
				Messages = new List<ChatMessage>
				{
					ChatMessage.FromSystem("You are a helpful assistant."),
					ChatMessage.FromUser("Who won the world series in 2020?"),
					ChatMessage.FromAssistant("The Los Angeles Dodgers won the World Series in 2020."),
					ChatMessage.FromUser("Where was it played?")
				},
				Model = Models.Gpt_4
			});

			if (completionResult.Successful)
			{
				return "success";
			}

			return "fail";
		}
	}
}
