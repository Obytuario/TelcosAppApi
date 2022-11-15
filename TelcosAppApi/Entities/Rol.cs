using System;
using System.Collections.Generic;

namespace TelcosAppApi.Entities;

public partial class Rol
{
    public Guid ID { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public virtual ICollection<OpcionModulo> OpcionModulo { get; } = new List<OpcionModulo>();

    public virtual ICollection<Usuario> Usuario { get; } = new List<Usuario>();
}
