using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class MovimientoEquipo
{
    public Guid ID { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<DetalleEquipoOrdenTrabajo> DetalleEquipoOrdenTrabajo { get; } = new List<DetalleEquipoOrdenTrabajo>();

    public virtual ICollection<LogDetalleEquipoOrdenTrabajo> LogDetalleEquipoOrdenTrabajo { get; } = new List<LogDetalleEquipoOrdenTrabajo>();
}
