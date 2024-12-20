using System;
using System.Collections.Generic;
using System.Configuration;
using CRUDApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDApi.Data;

public partial class PeopleDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public PeopleDbContext(DbContextOptions<PeopleDbContext> options, IConfiguration configuration)
     : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<People> Peoples { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3214EC07386BBFE7");

            entity.ToTable("City");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.Province).WithMany(p => p.Cities)
                .HasForeignKey(d => d.IdProvince)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__City__IdProvince__398D8EEE");
        });

        modelBuilder.Entity<People>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__People__3214EC078D9989A0");

            entity.ToTable("People");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Nationality).HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasMaxLength(20);

            entity.HasOne(d => d.City).WithMany(p => p.Peoples)
                .HasForeignKey(d => d.IdCity)
                .HasConstraintName("FK__People__IdCity__3D5E1FD2");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Province__3214EC07B0A9F1C0");

            entity.ToTable("Province");

            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC077D250735");

            entity.ToTable("Role");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0779C07BC6");

            entity.ToTable("User");

            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("UserRoles"));
                //.HasForeignKey(d => d.RoleId)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK__User__RoleId__4BAC3F29");

            //entity.HasOne(d => d.Roles).WithMany(p => p.Users)
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
