using FluentValidation;
using FluentValidation.AspNetCore;
using InPositionChatBot.BusinessLogic.Commands.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace InPositionChatBot.BusinessLogic
{
	public static class ServicesConfiguration
	{
		public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
		{
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssemblyContaining<CreateMessageCommandValidator>();

			services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateMessageCommand).Assembly));

			return services;
		}
	}
}
