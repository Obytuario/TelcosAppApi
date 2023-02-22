using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class OrdenTrabajo
{
    public Guid ID { get; set; }

    public string NumeroOrden { get; set; } = null!;

    public DateTime FechaOrden { get; set; }

    public DateTime FechaRegistro { get; set; }

    public Guid Suscriptor { get; set; }

    public Guid EstadoOrden { get; set; }

    public Guid UsuarioRegistra { get; set; }

    public virtual EstadoOrdenTrabajo EstadoOrdenNavigation { get; set; } = null!;

    public virtual Suscriptor SuscriptorNavigation { get; set; } = null!;

    public virtual Usuario UsuarioRegistraNavigation { get; set; } = null!;
}
