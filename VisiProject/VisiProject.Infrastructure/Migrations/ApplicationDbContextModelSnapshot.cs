﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VisiProject.Infrastructure;

#nullable disable

namespace VisiProject.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.ConversationEntity", b =>
                {
                    b.Property<string>("ConversationId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("AdminId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<long>("CreationTimeUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsOnline")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastMessageId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("ConversationId");

                    b.HasIndex("AdminId");

                    b.HasIndex("ConversationId")
                        .IsUnique();

                    b.HasIndex("LastMessageId")
                        .IsUnique()
                        .HasFilter("[LastMessageId] IS NOT NULL");

                    b.HasIndex("SenderId");

                    b.ToTable("Conversations", (string)null);
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.MessageEntity", b =>
                {
                    b.Property<string>("MessageId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ConversationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<long>("CreationTimeUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("MessageId");

                    b.HasIndex("ConversationId");

                    b.HasIndex("MessageId")
                        .IsUnique();

                    b.HasIndex("SenderId");

                    b.ToTable("Messages", (string)null);
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.RoleEntity", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("RoleId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.UserConversationEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ConversationId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("UserId", "ConversationId");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId", "ConversationId")
                        .IsUnique();

                    b.ToTable("UserConversations", (string)null);
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.UserEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsBlocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsOnline")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.UserRoleEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.ConversationEntity", b =>
                {
                    b.HasOne("VisiProject.Infrastructure.Entities.UserEntity", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VisiProject.Infrastructure.Entities.MessageEntity", "LastMessage")
                        .WithOne()
                        .HasForeignKey("VisiProject.Infrastructure.Entities.ConversationEntity", "LastMessageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("VisiProject.Infrastructure.Entities.UserEntity", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("LastMessage");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.MessageEntity", b =>
                {
                    b.HasOne("VisiProject.Infrastructure.Entities.ConversationEntity", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VisiProject.Infrastructure.Entities.UserEntity", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.UserConversationEntity", b =>
                {
                    b.HasOne("VisiProject.Infrastructure.Entities.ConversationEntity", "Conversation")
                        .WithMany("UserConversations")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VisiProject.Infrastructure.Entities.UserEntity", "User")
                        .WithMany("UserConversations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("VisiProject.Infrastructure.Entities.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VisiProject.Infrastructure.Entities.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.ConversationEntity", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserConversations");
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.RoleEntity", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("VisiProject.Infrastructure.Entities.UserEntity", b =>
                {
                    b.Navigation("UserConversations");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}