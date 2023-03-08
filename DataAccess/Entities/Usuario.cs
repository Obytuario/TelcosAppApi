using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class Usuario
{
    public Guid ID { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string? Apellidos { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string? Salt { get; set; }

    public string Contraseña { get; set; } = null!;

    public bool GenerarContraseña { get; set; }

    public bool Activo { get; set; }

    public Guid? CentroOperacion { get; set; }

    public Guid? Cargo { get; set; }

    public Guid? UsuarioSuperior { get; set; }

    public string? NumeroContacto { get; set; }

    public string? Correo { get; set; }

    public Guid Rol { get; set; }

    public virtual Cargo? CargoNavigation { get; set; }

    public virtual CentroOperacion? CentroOperacionNavigation { get; set; }

    public virtual ICollection<DetalleEquipoOrdenTrabajo> DetalleEquipoOrdenTrabajo { get; } = new List<DetalleEquipoOrdenTrabajo>();

    public virtual ICollection<DetalleMaterialOrdenTrabajo> DetalleMaterialOrdenTrabajo { get; } = new List<DetalleMaterialOrdenTrabajo>();

    public virtual ICollection<Usuario> InverseUsuarioSuperiorNavigation { get; } = new List<Usuario>();

    public virtual ICollection<LogDetalleEquipoOrdenTrabajo> LogDetalleEquipoOrdenTrabajo { get; } = new List<LogDetalleEquipoOrdenTrabajo>();

    public virtual ICollection<LogDetalleMaterialOrdenTrabajo> LogDetalleMaterialOrdenTrabajo { get; } = new List<LogDetalleMaterialOrdenTrabajo>();

    public virtual ICollection<LogUsuario> LogUsuarioUsuarioModificaNavigation { get; } = new List<LogUsuario>();

    public virtual ICollection<LogUsuario> LogUsuarioUsuarioNavigation { get; } = new List<LogUsuario>();

    public virtual ICollection<OrdenTrabajo> OrdenTrabajo { get; } = new List<OrdenTrabajo>();

    public virtual Rol RolNavigation { get; set; } = null!;

    public virtual ICollection<UbicacionUsuario> UbicacionUsuario { get; } = new List<UbicacionUsuario>();

    public virtual Usuario? UsuarioSuperiorNavigation { get; set; }
}
