using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class DetalleImagenOrdenTrabajo
{
    public Guid ID { get; set; }

    public string UrlImagen { get; set; } = null!;

    public Guid OrdenTrabajo { get; set; }

    public Guid TipoImagen { get; set; }

    public bool Activo { get; set; }

    public virtual OrdenTrabajo OrdenTrabajoNavigation { get; set; } = null!;

    public virtual TipoImagen TipoImagenNavigation { get; set; } = null!;
}
