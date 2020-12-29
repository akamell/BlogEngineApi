using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BlogEngineApi.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations;
using BlogEngineApi.Posts.Domain;

namespace BlogEngineApi.Shared.Infrastructure.Persistence.EntityFramework
{
    public class BlogEngineDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public BlogEngineDbContext(DbContextOptions<BlogEngineDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }

    }
}