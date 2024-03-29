﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.workOrderManagement
{
    public class UpdateWorkOrderManagementDTO
    {
        public Guid IdWorkOrder { get; set; }

        public Guid? IdAssitant { get; set; }
        
        public Guid IdUser { get; set; }

        public List<ImageDto> Photos { get; set; }
        public List<string>? Activitys { get; set; }

        public SuppliesDTO Supplies { get; set; }
    }

    public class ImageDto
    {
        public string PhotoBase64String { get; set; }
        public Guid IdTipoPhoto { get; set; }
    }

    public class SuppliesDTO
    {
        public List<EquiptmentDto>? Equiptments { get; set; }
        public List<MaterialDto>? Materials { get; set; }
    }


    public class EquiptmentDto
    {
        public Guid ParamEquipoDto { get; set; }
        public string SerialDto { get; set; }
        public Guid IdMovimientoDto { get; set; }
    }

    public class MaterialDto
    {
        public string ParamMaterialDto { get; set; }
        public int CantidadDto { get; set; }
    }
    public class activitysDto
    {
        public string ParamMaterialDto { get; set; }
        public int CantidadDto { get; set; }
    }
}
