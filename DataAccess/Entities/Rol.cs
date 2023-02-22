using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class Rol
{
    public Guid ID { get; set; }

    public string Descripcion { get; set; } = null!;

    public Guid? RolSuperior { get; set; }

    public bool? Activo { get; set; }

    public string Codigo { get; set; } = null!;

    public virtual ICollection<Rol> InverseRolSuperiorNavigation { get; } = new List<Rol>();

    public virtual ICollection<OpcionModulo> OpcionModulo { get; } = new List<OpcionModulo>();

    public virtual Rol? RolSuperiorNavigation { get; set; }

    public virtual ICollection<Usuario> Usuario { get; } = new List<Usuario>();
}
