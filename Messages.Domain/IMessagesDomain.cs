using Messages.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messages.Domain
{
    public interface IMessagesDomain
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Connection> Connections { get; set; }
    }
}