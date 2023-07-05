using AplicationServices.Application.Contracts.Carpetas;
using AplicationServices.DTOs.Generics;
using AutoMapper;
using DomainServices.Domain.Contracts.Carpetas;
using DomainServices.Domain.Contracts.Roles;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<RequestResult<List<imageGenericDto>>> GetImageById(Guid ordenTrabajo)
        {
            try
            {               
               var imagenes = await _carpetasDomain.GetImageById(ordenTrabajo);
                List<string> base64 = new List<string>();
                List<imageGenericDto> images = new List<imageGenericDto>();

                foreach (var imagen in imagenes){
                    imageGenericDto imageGenericDto = new imageGenericDto();
                    imageGenericDto.Title = imagen.TipoImagenNavigation.Descripcion;
                    imageGenericDto.ThumbImage = String.Format("data:image/gif;base64,{0}",getImagePath(imagen.UrlImagen));
                    imageGenericDto.Image = imageGenericDto.ThumbImage;
                    images.Add(imageGenericDto);
                }            

                return RequestResult<List<imageGenericDto>>.CreateSuccessful(images);

            }
            catch (Exception ex)
            {
                return RequestResult<List<imageGenericDto>>.CreateError(ex.Message);
            }
        }
        #endregion

        #region method Private

        private string getImagePath(string url)
        {
            //var filePath = Path.Combine(_config["StoredFilesPath"],
            byte[] imageArray = System.IO.File.ReadAllBytes(url);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;          
        }
        #endregion
    }
}
