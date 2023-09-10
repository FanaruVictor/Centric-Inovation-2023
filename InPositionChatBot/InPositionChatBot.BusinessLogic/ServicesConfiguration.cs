using FluentValidation;
using FluentValidation.AspNetCore;
using InPositionChatBot.BusinessLogic.Commands.Messages;
using InPositionChatBot.BusinessLogic.Services.BetalgoOpenAI;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Extensions;

namespace InPositionChatBot.BusinessLogic
{
	public static class ServicesConfiguration
	{
		public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
		{
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssemblyContaining<CreateMessageCommandValidator>();

			services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateMessageCommand).Assembly));

			services.AddOpenAIService();

			services.AddScoped<IBetalgoOpenAIService, BetalgoOpenAIService>();
			return services;
		}
	}
}
