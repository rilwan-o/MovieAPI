using AutoMapper;
using MovieAPI.Application.Features.ViewModels;
using MovieAPI.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Application.profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieVm>()
                    .ReverseMap();         
        }
    }
}
