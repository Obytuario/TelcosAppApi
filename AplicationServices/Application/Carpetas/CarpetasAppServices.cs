using AplicationServices.Application.Contracts.Carpetas;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.workOrderManagement;
using AutoMapper;
using Azure.Core;
using DomainServices.Domain.Contracts.Carpetas;
using DomainServices.Domain.Contracts.Roles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
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
        public async Task<RequestResult<string>> UploadImageByWorkOrder(ImageDto imageDto,string path)
        {
            try
            {
                var urlFile = convertBase64toPath(imageDto.PhotoBase64String, path);
                return RequestResult<string>.CreateSuccessful(urlFile);

            }
            catch (Exception ex)
            {
                return RequestResult<string>.CreateError(ex.Message);
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
        private string convertBase64toPath(string base64, string path)
        {     
           
            //Variable donde se coloca la ruta relativa de la carpeta de destino
            //del archivo cargado
            string NombreCarpeta = "Operaciones\\";            

            //Se concatena las variables "RutaRaiz" y "NombreCarpeta"
            //en una otra variable "RutaCompleta"
            string RutaCompleta = path + NombreCarpeta;


            //Se valida con la variable "RutaCompleta" si existe dicha carpeta            
            if (!Directory.Exists(RutaCompleta))
            {
                //En caso de no existir se crea esa carpeta
                Directory.CreateDirectory(RutaCompleta);
            }

           
                //Se declara en esta variable el nombre del archivo cargado
                string NombreArchivo = string.Concat(Guid.NewGuid().ToString(),".jpg");

                //Se declara en esta variable la ruta completa con el nombre del archivo
                string RutaFullCompleta = Path.Combine(RutaCompleta, NombreArchivo);

                File.WriteAllBytes(RutaFullCompleta, Convert.FromBase64String(base64));

            return RutaFullCompleta;

           
        }
        #endregion
    }
}
