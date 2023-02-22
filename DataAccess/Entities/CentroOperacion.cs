using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class CentroOperacion
{
    public Guid ID { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public bool Activo { get; set; }

    public string Longitud { get; set; } = null!;

    public string Latitud { get; set; } = null!;

    public virtual ICollection<LogUsuario> LogUsuario { get; } = new List<LogUsuario>();

    public virtual ICollection<Usuario> Usuario { get; } = new List<Usuario>();
}
