using Microsoft.EntityFrameworkCore;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<GenericObject> GenericObjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Set decimal types to decimal(18,2) in database (number length 18 total => 16 integer + 2 decimal point)
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            //Redundancy relationship just in case

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.Post)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete

            builder.Entity<Post>()
                .HasOne(p => p.Author) // Post to User (each post has 1 user)
                .WithMany(p => p.Posts) // User to Post (each user has many posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(c => c.Post) //Each comment has one post
                .WithMany(c => c.Comments) //Each post has many comment
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
