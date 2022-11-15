using System;
using System.Collections.Generic;

namespace TelcosAppApi.Entities;

public partial class Modulo
{
    public Guid ID { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public virtual ICollection<OpcionModulo> OpcionModulo { get; } = new List<OpcionModulo>();
}
