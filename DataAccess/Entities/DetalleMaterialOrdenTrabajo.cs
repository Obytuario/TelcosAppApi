using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class DetalleMaterialOrdenTrabajo
{
    public Guid ID { get; set; }

    public int Cantidad { get; set; }

    public Guid OrdenTrabajo { get; set; }

    public Guid ParamMaterialActividad { get; set; }

    public bool Activo { get; set; }

    public Guid UsuarioRegistra { get; set; }

    public DateTime FechaHoraRegistra { get; set; }

    public virtual ICollection<LogDetalleMaterialOrdenTrabajo> LogDetalleMaterialOrdenTrabajo { get; } = new List<LogDetalleMaterialOrdenTrabajo>();

    public virtual OrdenTrabajo OrdenTrabajoNavigation { get; set; } = null!;

    public virtual ParamMaterialActividad ParamMaterialActividadNavigation { get; set; } = null!;

    public virtual Usuario UsuarioRegistraNavigation { get; set; } = null!;
}
