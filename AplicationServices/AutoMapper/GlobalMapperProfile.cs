using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelcosAppApi.AplicationServices.AutoMapper
{
    public class GlobalMapperProfile:Profile
    {
        public GlobalMapperProfile()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
            });

            configuration.CompileMappings();
            configuration.AssertConfigurationIsValid();
        }
    }
}
