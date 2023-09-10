using InPositionChatBot.BusinessLogic.Commands.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InPositionChatBot.API.Controllers;

[ApiController, Route("api/[controller]")]
public class MessagesController : Controller
{
	private readonly IMediator _mediator;

	public MessagesController(IMediator mediator)
	{
		_mediator = mediator;
	}


	[HttpPost]
	public async Task<bool> Create([FromBody] CreateMessageCommand command)
	{
		return await _mediator.Send(command);
	}
}

