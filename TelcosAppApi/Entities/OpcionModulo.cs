using System;
using System.Collections.Generic;

namespace TelcosAppApi.Entities;

public partial class OpcionModulo
{
    public Guid ID { get; set; }

    public string Descripcion { get; set; } = null!;

    public Guid? Rol { get; set; }

    public Guid? Modulo { get; set; }

    public string Codigo { get; set; } = null!;

    public virtual Modulo? ModuloNavigation { get; set; }

    public virtual Rol? RolNavigation { get; set; }
}
