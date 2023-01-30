using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class Usuario
{
    public Guid ID { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string? Salt { get; set; }

    public string Contraseña { get; set; } = null!;

    public Guid Rol { get; set; }

    public virtual ICollection<OrdenTrabajo> OrdenTrabajo { get; } = new List<OrdenTrabajo>();

    public virtual Rol RolNavigation { get; set; } = null!;
}
