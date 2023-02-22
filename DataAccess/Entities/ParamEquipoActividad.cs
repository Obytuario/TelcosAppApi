using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class ParamEquipoActividad
{
    public Guid ID { get; set; }

    public Guid Actividad { get; set; }

    public Guid Equipo { get; set; }

    public bool Activo { get; set; }

    public virtual Actividad ActividadNavigation { get; set; } = null!;

    public virtual ICollection<DetalleEquipoOrdenTrabajo> DetalleEquipoOrdenTrabajo { get; } = new List<DetalleEquipoOrdenTrabajo>();

    public virtual Equipo EquipoNavigation { get; set; } = null!;

    public virtual ICollection<LogDetalleEquipoOrdenTrabajo> LogDetalleEquipoOrdenTrabajo { get; } = new List<LogDetalleEquipoOrdenTrabajo>();
}
