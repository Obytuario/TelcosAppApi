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

        #region Method

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
