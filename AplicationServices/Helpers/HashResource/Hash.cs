using AplicationServices.DTOs.Generics;
using DomainServices.Domain.Contracts.User;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TelcosAppApi.DataAccess.Entities;

namespace AplicationServices.Helpers.HashResource
{
    public class Hash
    {
        
        public Hash()
        {
            
        }

        #region Public Methods
        /// <summary>
        /// Retorna un hash con su salt segun texto plano
        /// </summary>
        /// <param name="nameFile">texto plano</param>
        public RequestHash GetHash(string nameFile)
        {
            var salt = new byte[16];
            using (var Random = RandomNumberGenerator.Create())
            {
                Random.GetBytes(salt);  
            }

            return GetHash(nameFile,salt);            
        }

        /// <summary>
        /// Genera un hash con su salt
        /// </summary>
        /// <param name="nameFile">texto plano</param>
        /// <param name="salt">areglo bytes</param>
        public RequestHash GetHash(string nameFile,byte[] salt)
        {
            var subKey = KeyDerivation.Pbkdf2(nameFile, salt, KeyDerivationPrf.HMACSHA1, 10000, 32);
            var hash = Convert.ToBase64String(subKey);  
            return new RequestHash() { Hash = hash ,SaltHash =salt};

        }
        
        #endregion

    }
}
