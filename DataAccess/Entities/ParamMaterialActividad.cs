using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class ParamMaterialActividad
{
    public Guid ID { get; set; }

    public Guid Actividad { get; set; }

    public Guid Material { get; set; }

    public bool Activo { get; set; }

    public virtual Actividad ActividadNavigation { get; set; } = null!;

    public virtual ICollection<DetalleMaterialOrdenTrabajo> DetalleMaterialOrdenTrabajo { get; } = new List<DetalleMaterialOrdenTrabajo>();

    public virtual ICollection<LogDetalleMaterialOrdenTrabajo> LogDetalleMaterialOrdenTrabajo { get; } = new List<LogDetalleMaterialOrdenTrabajo>();

    public virtual Material MaterialNavigation { get; set; } = null!;
}
