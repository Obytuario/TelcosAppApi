using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class DetalleCancelacionOrden
{
    public Guid ID { get; set; }

    public Guid MotivoCancelacionOrden { get; set; }

    public DateTime FechaRegistroCancelacion { get; set; }

    public Guid UsuarioRegistra { get; set; }

    public virtual MotivoCancelacionOrden MotivoCancelacionOrdenNavigation { get; set; } = null!;

    public virtual ICollection<OrdenTrabajo> OrdenTrabajo { get; } = new List<OrdenTrabajo>();

    public virtual Usuario UsuarioRegistraNavigation { get; set; } = null!;
}
