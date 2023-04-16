using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class Suscriptor
{
    public Guid ID { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public Guid TipoSuscriptor { get; set; }

    public string? Direccion { get; set; }

    public string NumeroCuenta { get; set; } = null!;

    public virtual ICollection<OrdenTrabajo> OrdenTrabajo { get; } = new List<OrdenTrabajo>();

    public virtual TipoSuscriptor TipoSuscriptorNavigation { get; set; } = null!;
}
