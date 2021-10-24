﻿using Microsoft.EntityFrameworkCore;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.DAL.Entities.Chat;

namespace WriteMe.Database.DAL.Context
{
    public class WriteMeDatabase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; } 
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<GeneratedMessage> GeneratedMessages { get; set; }
        public DbSet<FriendshipApplication> FriendshipApplications { get; set; }

        public WriteMeDatabase(DbContextOptions<WriteMeDatabase> options) : base(options)
        {
        }
    }
}
