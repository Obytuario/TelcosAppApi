using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TelcosAppApi.DataAccess.Entities;

public partial class TelcosSuiteContext : DbContext
{
    public TelcosSuiteContext()
    {
    }

    public TelcosSuiteContext(DbContextOptions<TelcosSuiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Modulo> Modulo { get; set; }

    public virtual DbSet<OpcionModulo> OpcionModulo { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DW-P10697;Database=TelcosSuite;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OpcionModulo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ModuloNavigation).WithMany(p => p.OpcionModulo)
                .HasForeignKey(d => d.Modulo)
                .HasConstraintName("FK_OpcionModulo_Modulo");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.OpcionModulo)
                .HasForeignKey(d => d.Rol)
                .HasConstraintName("FK_OpcionModulo_Rol");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Contraseña).IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
