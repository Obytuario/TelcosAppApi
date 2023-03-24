﻿using AplicationServices.Application.Contracts.Location;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.Location;
using AplicationServices.DTOs.User;
using AplicationServices.Helpers.HashResource;
using AplicationServices.Helpers.TextResorce;
using AutoMapper;
using DomainServices.Domain.Contracts.Location;
using DomainServices.Domain.Contracts.Roles;
using DomainServices.Domain.Location;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Application.Location
{
    public class LocationAppServices: ILocationServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly ILocationDomain _locationDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public LocationAppServices(IMapper mapper,ILocationDomain locationDomain)
        {
            _locationDomain=locationDomain;
            _mapper = mapper;
        }

        /// <summary>
        ///     Guarda un locacion de usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        public async Task<RequestResult<string>> SaveLocationUser(LocationDto userDto)
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();
                var user = _mapper.Map<LocationDto, UbicacionUsuario>(userDto);
                SaveUserLocationValidations(ref errorMessageValidations, user);
                if (errorMessageValidations.Any())
                    return RequestResult<string>.CreateUnsuccessful(errorMessageValidations);



                _locationDomain.SaveLocationUser(user);
                return RequestResult<string>.CreateSuccessful(ResourceUserMsm.SucessFullLocation);

            }
            catch (Exception ex)
            {
                return RequestResult<string>.CreateError(ex.Message);
            }
        }
        #region Private Methods
        /// <summary>
        ///     valida los datos para crear un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        private void SaveUserLocationValidations(ref List<string> errorMessageValidations, UbicacionUsuario user)
        {
            if (string.IsNullOrEmpty(user.Longitud)|| string.IsNullOrEmpty(user.Latitud))
            {
                errorMessageValidations.Add(ResourceUserMsm.InvalidLocation);
            }

        }
        #endregion
    }
}