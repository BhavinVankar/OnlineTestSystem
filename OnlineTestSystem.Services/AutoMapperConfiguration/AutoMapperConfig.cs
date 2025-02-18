using AutoMapper;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services.AutoMapperConfiguration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AssessmentModel, AssessmentRequestModel>().ReverseMap();
            CreateMap<AssessmentRequestModel, SectionModel>().ReverseMap();
            CreateMap<UserModel, UpdateUserModel>().ReverseMap();
        }
    }
}
