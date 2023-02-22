using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class LogUsuario
{
    public Guid ID { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public bool Activo { get; set; }

    public Guid CentroOperacion { get; set; }

    public Guid Cargo { get; set; }

    public Guid Rol { get; set; }

    public DateTime FechaHoraModifica { get; set; }

    public Guid UsuarioModifica { get; set; }

    public Guid Usuario { get; set; }

    public virtual Cargo CargoNavigation { get; set; } = null!;

    public virtual CentroOperacion CentroOperacionNavigation { get; set; } = null!;

    public virtual Usuario UsuarioModificaNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
