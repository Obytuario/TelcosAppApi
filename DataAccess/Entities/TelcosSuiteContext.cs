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

    public virtual DbSet<Actividad> Actividad { get; set; }

    public virtual DbSet<Cargo> Cargo { get; set; }

    public virtual DbSet<Carpeta> Carpeta { get; set; }

    public virtual DbSet<CentroOperacion> CentroOperacion { get; set; }

    public virtual DbSet<DetalleEquipoOrdenTrabajo> DetalleEquipoOrdenTrabajo { get; set; }

    public virtual DbSet<DetalleImagenOrdenTrabajo> DetalleImagenOrdenTrabajo { get; set; }

    public virtual DbSet<DetalleMaterialOrdenTrabajo> DetalleMaterialOrdenTrabajo { get; set; }

    public virtual DbSet<Equipo> Equipo { get; set; }

    public virtual DbSet<EstadoOrdenTrabajo> EstadoOrdenTrabajo { get; set; }

    public virtual DbSet<LogDetalleEquipoOrdenTrabajo> LogDetalleEquipoOrdenTrabajo { get; set; }

    public virtual DbSet<LogDetalleMaterialOrdenTrabajo> LogDetalleMaterialOrdenTrabajo { get; set; }

    public virtual DbSet<LogUsuario> LogUsuario { get; set; }

    public virtual DbSet<Material> Material { get; set; }

    public virtual DbSet<Modulo> Modulo { get; set; }

    public virtual DbSet<MovimientoEquipo> MovimientoEquipo { get; set; }

    public virtual DbSet<OpcionModulo> OpcionModulo { get; set; }

    public virtual DbSet<OrdenTrabajo> OrdenTrabajo { get; set; }

    public virtual DbSet<ParamEquipoActividad> ParamEquipoActividad { get; set; }

    public virtual DbSet<ParamMaterialActividad> ParamMaterialActividad { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Suscriptor> Suscriptor { get; set; }

    public virtual DbSet<TipoImagen> TipoImagen { get; set; }

    public virtual DbSet<TipoSuscriptor> TipoSuscriptor { get; set; }

    public virtual DbSet<UbicacionUsuario> UbicacionUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividad>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.CarpetaNavigation).WithMany(p => p.Actividad)
                .HasForeignKey(d => d.Carpeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actividad_Carpeta");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Carpeta>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CentroOperacion>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Latitud)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Longitud)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleEquipoOrdenTrabajo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.FechaHoraRegistra).HasColumnType("datetime");
            entity.Property(e => e.Serial)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.MovimientoEquipoNavigation).WithMany(p => p.DetalleEquipoOrdenTrabajo)
                .HasForeignKey(d => d.MovimientoEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleEquipoOrdenTrabajo_MovimientoEquipo");

            entity.HasOne(d => d.OrdenTrabajoNavigation).WithMany(p => p.DetalleEquipoOrdenTrabajo)
                .HasForeignKey(d => d.OrdenTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleEquipoOrdenTrabajo_OrdenTrabajo");

            entity.HasOne(d => d.ParamEquipoActividadNavigation).WithMany(p => p.DetalleEquipoOrdenTrabajo)
                .HasForeignKey(d => d.ParamEquipoActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleEquipoOrdenTrabajo_ParamEquipoActividad");

            entity.HasOne(d => d.UsuarioRegistraNavigation).WithMany(p => p.DetalleEquipoOrdenTrabajo)
                .HasForeignKey(d => d.UsuarioRegistra)
                .HasConstraintName("FK_DetalleEquipoOrdenTrabajo_Usuario");
        });

        modelBuilder.Entity<DetalleImagenOrdenTrabajo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.OrdenTrabajoNavigation).WithMany(p => p.DetalleImagenOrdenTrabajo)
                .HasForeignKey(d => d.OrdenTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleImagenOrdenTrabajo_OrdenTrabajo");

            entity.HasOne(d => d.TipoImagenNavigation).WithMany(p => p.DetalleImagenOrdenTrabajo)
                .HasForeignKey(d => d.TipoImagen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleImagenOrdenTrabajo_TipoImagen");
        });

        modelBuilder.Entity<DetalleMaterialOrdenTrabajo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.FechaHoraRegistra).HasColumnType("datetime");

            entity.HasOne(d => d.OrdenTrabajoNavigation).WithMany(p => p.DetalleMaterialOrdenTrabajo)
                .HasForeignKey(d => d.OrdenTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleMaterialOrdenTrabajo_OrdenTrabajo");

            entity.HasOne(d => d.ParamMaterialActividadNavigation).WithMany(p => p.DetalleMaterialOrdenTrabajo)
                .HasForeignKey(d => d.ParamMaterialActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleMaterialOrdenTrabajo_ParamMaterialActividad");

            entity.HasOne(d => d.UsuarioRegistraNavigation).WithMany(p => p.DetalleMaterialOrdenTrabajo)
                .HasForeignKey(d => d.UsuarioRegistra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleMaterialOrdenTrabajo_Usuario");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

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

        modelBuilder.Entity<LogDetalleEquipoOrdenTrabajo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.FechaHoraModifica).HasColumnType("datetime");
            entity.Property(e => e.Serial)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.DetalleEquipoOrdenTRabajoNavigation).WithMany(p => p.LogDetalleEquipoOrdenTrabajo)
                .HasForeignKey(d => d.DetalleEquipoOrdenTRabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogDetalleEquipoOrdenTrabajo_DetalleEquipoOrdenTrabajo");

            entity.HasOne(d => d.MovimientoEquipoNavigation).WithMany(p => p.LogDetalleEquipoOrdenTrabajo)
                .HasForeignKey(d => d.MovimientoEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogDetalleEquipoOrdenTrabajo_MovimientoEquipo");

            entity.HasOne(d => d.ParamEquipoActividadNavigation).WithMany(p => p.LogDetalleEquipoOrdenTrabajo)
                .HasForeignKey(d => d.ParamEquipoActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogDetalleEquipoOrdenTrabajo_ParamEquipoActividad");

            entity.HasOne(d => d.UsuarioModificaNavigation).WithMany(p => p.LogDetalleEquipoOrdenTrabajo)
                .HasForeignKey(d => d.UsuarioModifica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogDetalleEquipoOrdenTrabajo_Usuario");
        });

        modelBuilder.Entity<LogDetalleMaterialOrdenTrabajo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.FechaHoraModifica).HasColumnType("datetime");

            entity.HasOne(d => d.DetalleMaterialOrdenTrabajoNavigation).WithMany(p => p.LogDetalleMaterialOrdenTrabajo)
                .HasForeignKey(d => d.DetalleMaterialOrdenTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogDetalleMaterialOrdenTrabajo_DetalleMaterialOrdenTrabajo");

            entity.HasOne(d => d.ParamMaterialActividadNavigation).WithMany(p => p.LogDetalleMaterialOrdenTrabajo)
                .HasForeignKey(d => d.ParamMaterialActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogDetalleMaterialOrdenTrabajo_ParamMaterialActividad");

            entity.HasOne(d => d.UsuarioModificaNavigation).WithMany(p => p.LogDetalleMaterialOrdenTrabajo)
                .HasForeignKey(d => d.UsuarioModifica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogDetalleMaterialOrdenTrabajo_Usuario");
        });

        modelBuilder.Entity<LogUsuario>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña).IsUnicode(false);
            entity.Property(e => e.FechaHoraModifica).HasColumnType("datetime");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salt).IsUnicode(false);

            entity.HasOne(d => d.CargoNavigation).WithMany(p => p.LogUsuario)
                .HasForeignKey(d => d.Cargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogUsuario_Cargo");

            entity.HasOne(d => d.CentroOperacionNavigation).WithMany(p => p.LogUsuario)
                .HasForeignKey(d => d.CentroOperacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogUsuario_CentroOperacion");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.LogUsuarioUsuarioNavigation)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogUsuario_Usuario");

            entity.HasOne(d => d.UsuarioModificaNavigation).WithMany(p => p.LogUsuarioUsuarioModificaNavigation)
                .HasForeignKey(d => d.UsuarioModifica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogUsuario_Usuario1");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
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

        modelBuilder.Entity<MovimientoEquipo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
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

            entity.HasOne(d => d.CarpetaNavigation).WithMany(p => p.OrdenTrabajo)
                .HasForeignKey(d => d.Carpeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenTrabajo_Carpeta");

            entity.HasOne(d => d.EstadoOrdenNavigation).WithMany(p => p.OrdenTrabajo)
                .HasForeignKey(d => d.EstadoOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenTrabajo_EstadoOrdenTrabajo");

            entity.HasOne(d => d.SuscriptorNavigation).WithMany(p => p.OrdenTrabajo)
                .HasForeignKey(d => d.Suscriptor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenTrabajo_Suscriptor");

            entity.HasOne(d => d.TecnicoAuxiliarNavigation).WithMany(p => p.OrdenTrabajoTecnicoAuxiliarNavigation)
                .HasForeignKey(d => d.TecnicoAuxiliar)
                .HasConstraintName("FK_OrdenTrabajo_Usuario");

            entity.HasOne(d => d.UsuarioRegistraNavigation).WithMany(p => p.OrdenTrabajoUsuarioRegistraNavigation)
                .HasForeignKey(d => d.UsuarioRegistra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenTrabajo_Usuario1");
        });

        modelBuilder.Entity<ParamEquipoActividad>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.ActividadNavigation).WithMany(p => p.ParamEquipoActividad)
                .HasForeignKey(d => d.Actividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParamEquipoActividad_Actividad");

            entity.HasOne(d => d.EquipoNavigation).WithMany(p => p.ParamEquipoActividad)
                .HasForeignKey(d => d.Equipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParamEquipoActividad_Equipo");
        });

        modelBuilder.Entity<ParamMaterialActividad>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.ActividadNavigation).WithMany(p => p.ParamMaterialActividad)
                .HasForeignKey(d => d.Actividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParamMaterialActividad_Actividad");

            entity.HasOne(d => d.MaterialNavigation).WithMany(p => p.ParamMaterialActividad)
                .HasForeignKey(d => d.Material)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParamMaterialActividad_Material");
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

            entity.HasOne(d => d.RolSuperiorNavigation).WithMany(p => p.InverseRolSuperiorNavigation)
                .HasForeignKey(d => d.RolSuperior)
                .HasConstraintName("FK_Rol_Rol");
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

        modelBuilder.Entity<TipoImagen>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
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

        modelBuilder.Entity<UbicacionUsuario>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.Latitud)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Longitud)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.UbicacionUsuario)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UbicacionUsuario_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña).IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NumeroContacto)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salt).IsUnicode(false);

            entity.HasOne(d => d.CargoNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.Cargo)
                .HasConstraintName("FK_Usuario_Cargo");

            entity.HasOne(d => d.CentroOperacionNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.CentroOperacion)
                .HasConstraintName("FK_Usuario_CentroOperacion");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");

            entity.HasOne(d => d.UsuarioSuperiorNavigation).WithMany(p => p.InverseUsuarioSuperiorNavigation)
                .HasForeignKey(d => d.UsuarioSuperior)
                .HasConstraintName("FK_Usuario_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
