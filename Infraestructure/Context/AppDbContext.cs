using discagram.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace discagram.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
    
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure (EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id).HasName("PK_User");
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.UserName).IsUnique();
            
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasMaxLength(15);
        }
    }
}
