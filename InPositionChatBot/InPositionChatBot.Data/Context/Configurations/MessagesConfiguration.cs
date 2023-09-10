using InPositionChatBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tempus.Data.Context.Configurations;

public class MessagesConfiguration : IEntityTypeConfiguration<Message>
{
	public void Configure(EntityTypeBuilder<Message> builder)
	{
		builder.Property(x => x.Id).IsRequired();
		builder.Property(x => x.Sender).IsRequired();
		builder.Property(x => x.Text).IsRequired();
		builder.Property(x => x.Date).IsRequired();
	}
}