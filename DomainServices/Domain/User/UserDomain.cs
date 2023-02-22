using DomainServices.Domain.Contracts.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        ///     obtiene usuario por numero de documento
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="user">entidad usuario para obtener los datos</param>
        public async Task<Usuario> GetUser(string numeroDocumento)
        {
            return await _context.Usuario.Where(x => x.NumeroDocumento.Equals(numeroDocumento)).FirstOrDefaultAsync();
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
        #endregion|
    }
}
