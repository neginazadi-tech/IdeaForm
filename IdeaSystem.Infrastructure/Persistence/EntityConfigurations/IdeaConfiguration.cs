using IdeaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeaSystem.Infrastructure.Persistence.EntityConfigurations;

public class IdeaConfiguration : IEntityTypeConfiguration<Idea>
{
    protected string Schema => "dbo";
    protected string TableName => $"{nameof(Idea)}s";

    public void Configure(EntityTypeBuilder<Idea> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Title)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(i => i.Message)
        .HasMaxLength(1000);
    }
}
