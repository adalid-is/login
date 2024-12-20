//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace CRUDApi.Models;

//public partial class PersonaContext : DbContext
//{
//    public PersonaContext()
//    {
//    }

//    public PersonaContext(DbContextOptions<PersonaContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Role> Roles { get; set; }

//    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Persona;Trusted_Connection=True;TrustServerCertificate=True");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Role>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC077D250735");

//            entity.ToTable("Role");

//            entity.Property(e => e.Description).HasMaxLength(100);
//        });

//        modelBuilder.Entity<User>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0779C07BC6");

//            entity.ToTable("User");

//            entity.Property(e => e.Password).HasMaxLength(255);
//            entity.Property(e => e.Username).HasMaxLength(50);

//            entity.HasOne(d => d.Role).WithMany(p => p.Users)
//                .HasForeignKey(d => d.RoleId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__User__RoleId__4BAC3F29");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
