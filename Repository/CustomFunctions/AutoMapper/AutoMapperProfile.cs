﻿using AutoMapper;
using Repository.Data;
using Repository.Data.Entities;
using Repository.DTO;
using Repository.DTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CustomFunctions.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<List<User>, UserViewModel>().ReverseMap();
        }
    }
}
