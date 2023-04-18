using DomainServices.Domain.Contracts.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace DomainServices.Domain.User
{
    public class UserDomain: IUserDomain
    {
        private readonly TelcosSuiteContext _context;
        public UserDomain(TelcosSuiteContext telcosApiContext)
        {
            _context = telcosApiContext;
        }

        #region Public Methods
        /// <summary>
        ///     obtiene lista de usurios activoa
        /// </summary>
        /// <author>Ariel Bejarano</author>       
        public async Task<List<Usuario>> GetAllUser()
        {
            return await _context.Usuario.Where(x => x.Activo == true)
                                         .Include(i => i.RolNavigation)
                                         .Include(i => i.CargoNavigation)
                                         .Include(i => i.UsuarioSuperiorNavigation)
                                         .Include(i => i.CentroOperacionNavigation).ToListAsync();
        }
        /// <summary>
         ///     obtiene lista de usuarios asignados
         /// </summary>
         /// <author>Ariel Bejarano</author>       
        public async Task<List<Usuario>> GetUserAssignmentById(Guid user)
        {
            return await _context.Usuario.Where(x => x.Activo == true)
                                         .Include(i => i.RolNavigation)
                                         .Include(i => i.CargoNavigation)
                                         .Include(i => i.CentroOperacionNavigation)
                                         .Where(x => x.UsuarioSuperior == user).ToListAsync();
        }
        /// <summary>
        ///     obtiene lista de usuarios asignados
        /// </summary>
        /// <author>Ariel Bejarano</author>       
        public async Task<List<Usuario>> GetUserAssignmentByRol(Guid rol)
        {
            var resultRoles = await _context.Rol.Where(x => x.Activo == true)
                                        .Include(i => i.RolSuperiorNavigation.Usuario)
                                        .Include(i => i.InverseRolSuperiorNavigation).ThenInclude(t => t.Usuario)
                                        .Include(i => i.Usuario)
                                        .Where(x => x.RolSuperiorNavigation.ID == rol).ToListAsync();

            List<Usuario> usuarios = new List<Usuario>();

            validateUsers(resultRoles);

            void validateUsers(List<Rol> roles)
            {
                roles.ForEach(rol => {
                    usuarios.AddRange(rol.Usuario);                    
                });
            }

            return usuarios;

            //return await _context.Usuario.Where(x => x.Activo == true)
            //                             .Include(i => i.RolNavigation.RolSuperiorNavigation.Usuario)
            //                             .Include(i => i.RolNavigation.InverseRolSuperiorNavigation)
            //                             .Include(i => i.CargoNavigation)
            //                             .Include(i => i.CentroOperacionNavigation)
            //                             .Where(x => x.InverseUsuarioSuperiorNavigation.All(x => x.ID == rol)).ToListAsync();
        }
        /// <summary>
        ///     obtiene lista de usuarios asignados
        /// </summary>
        /// <author>Ariel Bejarano</author>       
        public List<Usuario> GetUserAssignmentByRol2(Guid rol)
        {
            var otro = _context.Rol.Where(x => x.Activo == true)
                                        .Include(i => i.RolSuperiorNavigation.Usuario)
                                        .Include(i => i.InverseRolSuperiorNavigation).ThenInclude(t => t.Usuario)
                                        .Include(i => i.Usuario)
                                        .Where(x => x.RolSuperiorNavigation.ID == rol).ToList();

            List<Usuario> usuarios = new List<Usuario>();

            validateUsers(otro);

            void validateUsers(List<Rol> roles)
            {
                roles.ForEach(rol => {
                    usuarios.AddRange(rol.Usuario);
                    //if (rol.InverseRolSuperiorNavigation.Count > 0)
                    //    validateUsers(rol.InverseRolSuperiorNavigation.ToList());
                });
            }

            
            return null;
        }
        /// <summary>
        ///     obtiene usuario por numero de documento
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">entidad usuario para obtener los datos</param>
        public async Task<Usuario> GetUser(string numeroDocumento)
        {            
            return await _context.Usuario.Include(x => x.RolNavigation)
                                         .Include(x => x.CargoNavigation).Where(x => x.NumeroDocumento.Equals(numeroDocumento)).FirstOrDefaultAsync();
        }
        /// <summary>
        ///     obtiene usuario por id
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">entidad usuario para obtener los datos</param>
        public async Task<Usuario> GetUserById(Guid user)
        {
            return await _context.Usuario.Where(x => x.ID.Equals(user)).FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Guarda un usuario
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">objeto para guardar un usuario</param>
        public void SaveUser(Usuario user)
        {
            _context.Usuario.Add(user);
            _context.SaveChanges();
        }
        /// <summary>
        ///     actualiza un usuario
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">objeto para Actualizar un usuario</param>
        public void UpdateUser(Usuario userUpdate, Usuario user)
        {
            user.Contraseña = userUpdate.Contraseña;
            user.Salt = userUpdate.Salt;
            _context.Entry(userUpdate).CurrentValues.SetValues(user);           
            _context.SaveChanges();
        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema por Rol
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        public async Task<List<Usuario>> GetAllUsersByRolCode(string code)
        {
            return await _context.Usuario.Where(x => x.Activo == true && x.RolNavigation.Codigo == code)
                                         .Include(i => i.RolNavigation)
                                         .Include(i => i.CargoNavigation)
                                         .Include(i => i.UsuarioSuperiorNavigation)
                                         .Include(i => i.CentroOperacionNavigation).ToListAsync();
        }

        #endregion|
    }
}
