using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogEngineApi.Posts.Domain;
using BlogEngineApi.Shared.Infrastructure.Persistence.EntityFramework;

namespace BlogEngineApi.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("post");

            builder.HasKey(x => x.PostID)
                .HasName("postID");

            /*builder.Property(p => p.PostID)
                .HasColumnType("integer")
                .HasColumnName("postID")
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn();*/

            builder.Property(p => p.Content)
                .HasMaxLength(256)
                .HasColumnName("content")
                .IsRequired(true);

            builder.Property(p => p.Author)
                .HasMaxLength(50)
                .HasColumnName("author")
                .IsRequired(true);

            builder.Property(p => p.Status)
                .HasColumnType("integer")
                .HasColumnName("status")
                .IsRequired(true);

            builder.Property(p => p.Publish)
                .HasColumnType("timestamp")
                .HasColumnName("publish")
                .IsRequired(false);

            builder.Property(p => p.Approval)
                .HasColumnType("timestamp")
                .HasColumnName("approval")
                .IsRequired(false);
        }
    }
}