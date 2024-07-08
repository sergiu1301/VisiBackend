using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisiProject.Infrastructure.Entities;

namespace VisiProject.Infrastructure.EntityConfigurations;

internal class ConversationEntityConfiguration : IEntityTypeConfiguration<ConversationEntity>
{
    public void Configure(EntityTypeBuilder<ConversationEntity> builder)
    {
        builder.ToTable("Conversations");

        builder.HasKey(s => s.ConversationId);

        builder.HasIndex(s => s.ConversationId)
            .IsUnique();

        builder.Property(s => s.ConversationId)
            .HasMaxLength(36);

        builder.Property(s => s.AdminId)
            .HasMaxLength(36);

        builder.Property(s => s.SenderId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(s => s.IsOnline)
            .HasDefaultValue(false);

        builder.Property(s => s.GroupName)
            .HasMaxLength(128);

        builder.Property(s => s.CreationTimeUnix);

        builder.HasMany(r => r.UserConversations)
            .WithOne(ur => ur.Conversation)
            .HasForeignKey(ur => ur.ConversationId);

        builder.HasOne(c => c.Admin)
            .WithMany()
            .HasForeignKey(c => c.AdminId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Sender)
            .WithMany()
            .HasForeignKey(c => c.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.LastMessage)
            .WithOne()
            .HasForeignKey<ConversationEntity>(c => c.LastMessageId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Conversation)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}