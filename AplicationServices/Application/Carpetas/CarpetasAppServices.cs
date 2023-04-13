using AplicationServices.Application.Contracts.Carpetas;
using AplicationServices.DTOs.Generics;
using AutoMapper;
using DomainServices.Domain.Contracts.Carpetas;
using DomainServices.Domain.Contracts.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Application.Carpetas
{
    public class CarpetasAppServices:ICarpetasServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly ICarpetaDomain _carpetasDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public CarpetasAppServices(ICarpetaDomain carpetasDomain, IMapper mapper)
        {
            _carpetasDomain = carpetasDomain;
            _mapper = mapper;
        }
        #region Method
        public async Task<RequestResult<List<GenericDto>>> GetFiles()
        {
            try
            {
                return RequestResult<List<GenericDto>>.CreateSuccessful(_mapper.Map<List<Carpeta>, List<GenericDto>>(await _carpetasDomain.GetCarpetas()));


            }
            catch (Exception ex)
            {
                return RequestResult<List<GenericDto>>.CreateError(ex.Message);
            }
        }
        public async Task<RequestResult<List<paramGenericDto>>> GetActyvitiEquipmentByFile(Guid file)
        {
            try
            {
                return RequestResult<List<paramGenericDto>>.CreateSuccessful(_mapper.Map<List<ParamEquipoActividad>, List<paramGenericDto>>(await _carpetasDomain.GetActyvitiEquipmentByFile(file)));


            }
            catch (Exception ex)
            {
                return RequestResult<List<paramGenericDto>>.CreateError(ex.Message);
            }
        }
        public async Task<RequestResult<List<paramGenericDto>>> GetActyvitiMaterialByFile(Guid file)
        {
            try
            {
                return RequestResult<List<paramGenericDto>>.CreateSuccessful(_mapper.Map<List<ParamMaterialActividad>, List<paramGenericDto>>(await _carpetasDomain.GetActyvitiMaterialByFile(file)));


            }
            catch (Exception ex)
            {
                return RequestResult<List<paramGenericDto>>.CreateError(ex.Message);
            }
        }
        public async Task<RequestResult<List<paramGenericDto>>> GetEquipmentByActivity(Guid activity)
        {
            try
            {
                return RequestResult<List<paramGenericDto>>.CreateSuccessful(_mapper.Map<List<ParamEquipoActividad>, List<paramGenericDto>>(await _carpetasDomain.GetEquipmentByActivity(activity)));


            }
            catch (Exception ex)
            {
                return RequestResult<List<paramGenericDto>>.CreateError(ex.Message);
            }
        }
        public async Task<RequestResult<List<paramGenericDto>>> GetMaterialByActivity(Guid activity)
        {
            try
            {
                return RequestResult<List<paramGenericDto>>.CreateSuccessful(_mapper.Map<List<ParamMaterialActividad>, List<paramGenericDto>>(await _carpetasDomain.GetMaterialByActivity(activity)));


            }
            catch (Exception ex)
            {
                return RequestResult<List<paramGenericDto>>.CreateError(ex.Message);
            }
        }
        #endregion
    }
}
