using Microsoft.EntityFrameworkCore;
using TelcosAppApi.DataAccess.Entities;

namespace TelcosAppApi.DataAccess.DataAccess
{
    public class TelcosApiContext: TelcosSuiteContext
    {
        public TelcosApiContext(CustomDbSettings options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstadoOrdenTrabajo>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.Codigo)
                    .HasMaxLength(6)
                    .IsUnicode(false);
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

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

            modelBuilder.Entity<OrdenTrabajo>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.FechaOrden).HasColumnType("datetime");
                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
                entity.Property(e => e.NumeroOrden)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.EstadoOrdenNavigation).WithMany(p => p.OrdenTrabajo)
                    .HasForeignKey(d => d.EstadoOrden)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenTrabajo_EstadoOrdenTrabajo");

                entity.HasOne(d => d.SuscriptorNavigation).WithMany(p => p.OrdenTrabajo)
                    .HasForeignKey(d => d.Suscriptor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenTrabajo_Suscriptor");

                entity.HasOne(d => d.UsuarioRegistraNavigation).WithMany(p => p.OrdenTrabajo)
                    .HasForeignKey(d => d.UsuarioRegistra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenTrabajo_Usuario");
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

            modelBuilder.Entity<Suscriptor>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.NumeroCuenta)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.TipoSuscriptorNavigation).WithMany(p => p.Suscriptor)
                    .HasForeignKey(d => d.TipoSuscriptor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Suscriptor_TipoSuscriptor");
            });

            modelBuilder.Entity<TipoSuscriptor>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
                entity.Property(e => e.Codigo)
                    .HasMaxLength(6)
                    .IsUnicode(false);
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
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
                entity.Property(e => e.Salt).IsUnicode(false);

                entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Rol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            //OnModelCreatingPartial(modelBuilder);
        }
    }
}
