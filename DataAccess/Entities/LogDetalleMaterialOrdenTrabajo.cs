using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class LogDetalleMaterialOrdenTrabajo
{
    public Guid ID { get; set; }

    public int Cantidad { get; set; }

    public Guid ParamMaterialActividad { get; set; }

    public bool Activo { get; set; }

    public Guid UsuarioModifica { get; set; }

    public string? ObservacionModifica { get; set; }

    public DateTime FechaHoraModifica { get; set; }

    public Guid DetalleMaterialOrdenTrabajo { get; set; }

    public virtual DetalleMaterialOrdenTrabajo DetalleMaterialOrdenTrabajoNavigation { get; set; } = null!;

    public virtual ParamMaterialActividad ParamMaterialActividadNavigation { get; set; } = null!;

    public virtual Usuario UsuarioModificaNavigation { get; set; } = null!;
}
