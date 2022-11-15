using System;
using System.Collections.Generic;

namespace TelcosAppApi.Entities;

public partial class Usuario
{
    public Guid ID { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public Guid Rol { get; set; }

    public virtual Rol RolNavigation { get; set; } = null!;
}
