﻿using System;
using System.Collections.Generic;

namespace TelcosAppApi.DataAccess.Entities;

public partial class Actividad
{
    public Guid ID { get; set; }

    public byte[] Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public byte[] Codigo { get; set; } = null!;

    public Guid Carpeta { get; set; }

    public virtual Carpeta CarpetaNavigation { get; set; } = null!;

    public virtual ICollection<ParamEquipoActividad> ParamEquipoActividad { get; } = new List<ParamEquipoActividad>();

    public virtual ICollection<ParamMaterialActividad> ParamMaterialActividad { get; } = new List<ParamMaterialActividad>();
}