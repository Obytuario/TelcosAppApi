using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class TipoImagen
{
    public Guid ID { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public string Codigo { get; set; } = null!;

    public virtual ICollection<DetalleImagenOrdenTrabajo> DetalleImagenOrdenTrabajo { get; } = new List<DetalleImagenOrdenTrabajo>();
}
