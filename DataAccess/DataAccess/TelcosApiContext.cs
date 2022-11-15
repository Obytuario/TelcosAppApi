using Microsoft.EntityFrameworkCore;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.DataAccess.DataAccess
{
    public class TelcosApiContext: TelcosSuiteContext
    {
        public TelcosApiContext(DbContextOptions options)
        {

        }
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
            
        }
    }
}
