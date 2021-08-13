using AutoMapper;
using Glamz.Business.Entity;
using Glamz.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glamz.Business.API
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("Profile")
        {

        }

        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            CreateMap<AuthenticateRequestDto, User>();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<BusinessDto, Entity.Business>().ReverseMap();
        }
    }
}