using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class UbicacionUsuario
{
    public Guid ID { get; set; }

    public Guid Usuario { get; set; }

    public string Longitud { get; set; } = null!;

    public string Latitud { get; set; } = null!;

    public DateTime FechaHora { get; set; }

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
