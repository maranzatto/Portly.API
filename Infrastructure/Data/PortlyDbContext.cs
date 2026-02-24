using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portly.Domain.Entities;
using Portly.Domain.ValueObjects;

namespace Portly.Infrastructure.Data;

public class PortlyDbContext : DbContext
{
    public DbSet<Visitor> Visitors { get; set;  }
    public DbSet<User> Users{ get; set; }
    public DbSet<Resident> Residents { get; set; }

    public PortlyDbContext(DbContextOptions<PortlyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var documentConverter = new ValueConverter<Document, string>(
            document => document.Value,
            value => Document.Create(value)
        );

        modelBuilder.Entity<Visitor>(builder =>
        {
            builder.ToTable("visitors");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(v => v.Document)
                .HasConversion(documentConverter)
                .HasColumnName("document")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(v => v.Phone)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(v => v.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(v => v.CreatedAt)
                .IsRequired();

            builder.Property(v => v.UpdatedAt)
                .IsRequired();

            builder.Property(v => v.IsDeleted)
                .IsRequired();

            builder.HasIndex(v => v.Email).IsUnique();
            builder.HasIndex(v => v.Document).IsUnique();
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Resident>(builder =>
        {
            builder.ToTable("residents");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(r => r.Document)
                .HasConversion(documentConverter)
                .HasColumnName("document")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(r => r.Phone)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(r => r.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(r => r.Apartment)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(r => r.Block)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.UpdatedAt)
                .IsRequired();

            builder.Property(r => r.IsDeleted)
                .IsRequired();

            builder.HasIndex(r => r.Email).IsUnique();
            builder.HasIndex(r => r.Document).IsUnique();

            builder.HasMany(r => r.Visitors)
                .WithOne()
                .HasForeignKey("ResidentId")
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

