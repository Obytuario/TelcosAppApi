using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class Carpeta
{
    public Guid ID { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Actividad> Actividad { get; } = new List<Actividad>();

    public virtual ICollection<OrdenTrabajo> OrdenTrabajo { get; } = new List<OrdenTrabajo>();
}
