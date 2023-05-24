using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class LogDetalleEquipoOrdenTrabajo
{
    public Guid ID { get; set; }

    public Guid ParamEquipoActividad { get; set; }

    public string Serial { get; set; } = null!;

    public Guid MovimientoEquipo { get; set; }

    public Guid DetalleEquipoOrdenTRabajo { get; set; }

    public bool Activo { get; set; }

    public Guid UsuarioModifica { get; set; }

    public string? ObservacionModifica { get; set; }

    public DateTime FechaHoraModifica { get; set; }

    public virtual DetalleEquipoOrdenTrabajo DetalleEquipoOrdenTRabajoNavigation { get; set; } = null!;

    public virtual MovimientoEquipo MovimientoEquipoNavigation { get; set; } = null!;

    public virtual ParamEquipoActividad ParamEquipoActividadNavigation { get; set; } = null!;

    public virtual Usuario UsuarioModificaNavigation { get; set; } = null!;
}
