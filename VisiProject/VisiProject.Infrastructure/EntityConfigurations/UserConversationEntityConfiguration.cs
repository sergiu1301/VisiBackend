using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VisiProject.Infrastructure.Entities;

namespace VisiProject.Infrastructure.EntityConfigurations;

internal class UserConversationEntityConfiguration : IEntityTypeConfiguration<UserConversationEntity>
{
    public void Configure(EntityTypeBuilder<UserConversationEntity> builder)
    {
        builder.ToTable("UserConversations");

        builder.HasKey(ur => new { ur.UserId, ur.ConversationId });

        builder.HasIndex(ur => new { ur.UserId, ur.ConversationId })
            .IsUnique();

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserConversations)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Conversation)
            .WithMany(r => r.UserConversations)
            .HasForeignKey(ur => ur.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}