using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisiProject.Infrastructure.Entities;

namespace VisiProject.Infrastructure.EntityConfigurations;

internal class MessageEntityConfiguration : IEntityTypeConfiguration<MessageEntity>
{
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.ToTable("Messages");

        builder.HasKey(s => s.MessageId);

        builder.HasIndex(s => s.MessageId)
            .IsUnique();

        builder.Property(s => s.MessageId)
            .HasMaxLength(36);

        builder.Property(s => s.Content)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(s => s.SenderId)
            .HasMaxLength(36);

        builder.Property(s => s.MessageType)
            .HasMaxLength(50);

        builder.Property(s => s.CreationTimeUnix);

        builder.HasOne(c => c.Sender)
            .WithMany()
            .HasForeignKey(c => c.SenderId);

        builder.HasOne(m => m.Conversation)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}