using AutoMapper;
using ShoppingTrolley.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingTrolley.Application.UnitTests.Common
{
    public class MappingTestFixture
    {
        public MappingTestFixture()
        {
            ConfigurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
