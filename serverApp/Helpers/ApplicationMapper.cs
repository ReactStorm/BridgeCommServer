using AutoMapper;
using serverApp.Data;
using serverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Products, ProductModel>().ReverseMap();
            CreateMap<Users, UserModel>().ReverseMap();
        }
    }
}
