using DomainServices.Domain.Contracts.Location;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.Location
{
    public class LocationDomain: ILocationDomain
    {
        private readonly TelcosSuiteContext _context;

        public LocationDomain(TelcosSuiteContext context)
        {
            _context = context;
        }

        #region Method
        /// <summary>
        ///     Guarda un locacion de un usuario
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">objeto para guardar un usuario</param>
        public void SaveLocationUser(UbicacionUsuario user)
        {
            _context.UbicacionUsuario.Add(user);
            _context.SaveChanges();
        }

     
        /// <summary>
        ///     obtiene ubicacion de usuarios segun su superior
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">objeto para guardar un usuario</param>
        public async Task<List<UbicacionUsuario>> GetLocationByUser(Guid user)
        {
            var resultUbicacion = await _context.UbicacionUsuario.Where(x => x.FechaHora.Date.Equals(DateTime.Now.Date))
                                        .Include(i => i.UsuarioNavigation).ToListAsync();
                                        
                                      

            //List<UbicacionUsuario> usuarios = new List<UbicacionUsuario>();

            //validateUsers(resultRoles);

            //void validateUsers(List<Rol> roles)
            //{
            //    roles.ForEach(rol => {
            //        usuarios.AddRange(rol.Usuario);
            //    });
            //}

            return resultUbicacion;
        }


        #endregion|
    }
}
