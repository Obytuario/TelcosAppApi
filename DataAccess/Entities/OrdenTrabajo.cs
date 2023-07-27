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

    public Guid Carpeta { get; set; }

    public Guid? CentroOperacion { get; set; }

    public Guid? TecnicoAuxiliar { get; set; }

    public string? Latitud { get; set; }

    public string? Logitud { get; set; }

    public string? Nodo { get; set; }

    public Guid? DetalleCancelacionOrden { get; set; }

    public Guid UsuarioRegistra { get; set; }

    public virtual Carpeta CarpetaNavigation { get; set; } = null!;

    public virtual DetalleCancelacionOrden? DetalleCancelacionOrdenNavigation { get; set; }

    public virtual ICollection<DetalleEquipoOrdenTrabajo> DetalleEquipoOrdenTrabajo { get; } = new List<DetalleEquipoOrdenTrabajo>();

    public virtual ICollection<DetalleImagenOrdenTrabajo> DetalleImagenOrdenTrabajo { get; } = new List<DetalleImagenOrdenTrabajo>();

    public virtual ICollection<DetalleMaterialOrdenTrabajo> DetalleMaterialOrdenTrabajo { get; } = new List<DetalleMaterialOrdenTrabajo>();

    public virtual EstadoOrdenTrabajo EstadoOrdenNavigation { get; set; } = null!;

    public virtual Suscriptor SuscriptorNavigation { get; set; } = null!;

    public virtual Usuario? TecnicoAuxiliarNavigation { get; set; }

    public virtual Usuario UsuarioRegistraNavigation { get; set; } = null!;
}
