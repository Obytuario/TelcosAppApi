using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class DetalleEquipoOrdenTrabajo
{
    public Guid ID { get; set; }

    public Guid OrdenTrabajo { get; set; }

    public Guid ParamEquipoActividad { get; set; }

    public string Serial { get; set; } = null!;

    public Guid MovimientoEquipo { get; set; }

    public bool Activo { get; set; }

    public Guid? UsuarioRegistra { get; set; }

    public DateTime? FechaHoraRegistra { get; set; }

    public virtual ICollection<LogDetalleEquipoOrdenTrabajo> LogDetalleEquipoOrdenTrabajo { get; } = new List<LogDetalleEquipoOrdenTrabajo>();

    public virtual MovimientoEquipo MovimientoEquipoNavigation { get; set; } = null!;

    public virtual OrdenTrabajo OrdenTrabajoNavigation { get; set; } = null!;

    public virtual ParamEquipoActividad ParamEquipoActividadNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioRegistraNavigation { get; set; }
}
