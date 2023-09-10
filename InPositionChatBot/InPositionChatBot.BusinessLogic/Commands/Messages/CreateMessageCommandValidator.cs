using FluentValidation;

namespace InPositionChatBot.BusinessLogic.Commands.Messages
{
	public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
	{
		public CreateMessageCommandValidator()
		{
			RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.NewGuid()).WithMessage("The Id of the message must be defined.");
			RuleFor(x => x.Text).NotEmpty().WithMessage("The message can not be empty");
			RuleFor(x => x.Sender).Must(x => x == Domain.Entities.Sender.User).WithMessage("An AI can not send messages");
		}
	}
}
